using HRSystem.Models;
using HRSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Services.Implementations
{
    public class DivisionService : IDivisionService
    {
        private readonly HrSystem _context;

        public DivisionService(HrSystem context)
        {
            _context = context;
        }

        public async Task<Division> GetByIdAsync(int divisionId)
        {
            return await _context.Divisions
                .Include(d => d.Parent)
                .FirstOrDefaultAsync(m => m.DivisionId == divisionId);
        }

        public async Task<List<Division>> GetListAsync()
        {
            return await _context.Divisions.Include(d => d.Parent).ToListAsync();
        }

        public async Task CreateAsync(Division division)
        {
            _context.Add(division);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Division division)
        {
            _context.Update(division);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int divisionId)
        {
            var division = await GetByIdAsync(divisionId);
            _context.Divisions.Remove(division);
            await _context.SaveChangesAsync();
        }
    }
}