
using HRSystem.Filters;
using HRSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Services.Interfaces
{
    public interface IDivisionService
    {
        public Task<Division> GetByIdAsync(int divisionId);
        
        public Task<List<Division>> GetListAsync(DivisionFilter filter = null);

        public Task CreateAsync(Division division);
        
        public Task UpdateAsync(Division division);
        
        public Task DeleteAsync(int divisionId);
    }
}