
using HRSystem.Filters;
using HRSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<Employee> GetByIdAsync(int employeeId);
        
        public Task<List<Employee>> GetListAsync();

        public Task CreateAsync(Employee employee);
        
        public Task UpdateAsync(Employee employee);
        
        public Task DeleteAsync(int employeeId);
    }
}