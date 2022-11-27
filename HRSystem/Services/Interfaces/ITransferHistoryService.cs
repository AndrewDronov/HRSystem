using HRSystem.Filters;
using HRSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRSystem.Services.Interfaces
{
    public interface ITransferHistoryService
    {
        public Task<List<TransferHistory>> GetListAsync(TransferHistoryFilter filter);

        public Task TrackTransferAsync(Employee oldEmployee, Employee newEmployee);
    }
}