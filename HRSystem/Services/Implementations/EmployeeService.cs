using HRSystem.Models;
using HRSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HrSystem _context;

        public EmployeeService(HrSystem context)
        {
            _context = context;
        }


        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            return await _context.Employees
                .Include(e => e.Division)
                .FirstOrDefaultAsync(m => m.EmployeeId == employeeId);
        }

        public async Task<List<Employee>> GetListAsync()
        {
            return await _context.Employees.Include(e => e.Division).ToListAsync();
        }

        public async Task CreateAsync(Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int employeeId)
        {
            var employee = await GetByIdAsync(employeeId);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}