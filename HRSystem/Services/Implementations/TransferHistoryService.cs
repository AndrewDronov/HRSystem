using HRSystem.Filters;
using HRSystem.Models;
using HRSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRSystem.Services.Implementations
{
    public class TransferHistoryService : ITransferHistoryService
    {
        private readonly HrSystem _context;

        public TransferHistoryService(HrSystem context)
        {
            _context = context;
        }

        public async Task<List<TransferHistory>> GetListAsync(TransferHistoryFilter filter)
        {
            var query = _context.TransferHistories.AsQueryable();

            if (filter != null)
            {
                if (filter.DivisionId != null)
                {
                    query = query.Where(th => th.DivisionId == filter.DivisionId);
                }

                if (filter.EmployeeId != null)
                {
                    query = query.Where(th => th.EmployeeId == filter.EmployeeId);
                }


                query = query.Where(th =>
                    (filter.DateFrom < th.DateTo && th.DateFrom < filter.DateTo) ||
                    (th.DateTo == null && filter.DateTo > th.DateFrom));
            }

            return await query.Include(d => d.Division)
                .Include(e => e.Employee)
                .ToListAsync();
        }

        public async Task TrackTransferAsync(Employee oldEmployee, Employee newEmployee)
        {
            if (oldEmployee?.DivisionId == newEmployee.DivisionId)
            {
                await Task.CompletedTask;
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (oldEmployee == null || oldEmployee.DivisionId == null && newEmployee.DivisionId != null)
                {
                    await CreateAsync(newEmployee);
                }
                else if (oldEmployee.DivisionId != null)
                {
                    var transferHistory = await _context
                        .TransferHistories
                        .FirstOrDefaultAsync(th => th.EmployeeId == newEmployee.EmployeeId && th.DateTo == null);

                    transferHistory.DateTo = DateTime.Now;
                    _context.Update(transferHistory);

                    if (newEmployee.DivisionId != null)
                    {
                        await CreateAsync(newEmployee);
                    }

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
            }
        }

        private async Task CreateAsync(Employee employee)
        {
            var transferHistory = new TransferHistory()
            {
                DivisionId = employee.DivisionId.Value,
                EmployeeId = employee.EmployeeId,
                DateFrom = DateTime.Now
            };

            _context.Add(transferHistory);

            await _context.SaveChangesAsync();
        }
    }
}