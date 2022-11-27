using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRSystem.Models;
using HRSystem.ViewModels;

namespace HRSystem.Controllers
{
    public class TransferHistoryController : Controller
    {
        private readonly HrSystem _context;

        public TransferHistoryController(HrSystem context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index([FromForm] TransferHistoryViewModel model)
        {
            var query = _context.TransferHistories.AsQueryable();

            if (model.Filter?.DivisionId != null)
            {
                query =  query.Where(th => th.DivisionId == model.Filter.DivisionId);
            }
            
            if (model.Filter?.EmployeeId != null)
            {
                query =  query.Where(th => th.EmployeeId == model.Filter.EmployeeId);
            }
            
            if (model.Filter?.DateFrom != null)
            {
                query =  query.Where(th => model.Filter.DateFrom <= th.CreatedAt);
            }
            
            if (model.Filter?.DateTo != null)
            {
                query =  query.Where(th => model.Filter.DateTo >= th.CreatedAt);
            }

            var data = await query.Include(d => d.Division)
                .Include(e => e.Employee)
                .ToListAsync();

            model.Data = data;

            
            return View(model);
        }
    }
}
