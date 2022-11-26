using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRSystem.Models;

namespace HRSystem.Controllers
{
    public class DivisionController : Controller
    {
        private readonly HrSystem _context;

        public DivisionController(HrSystem context)
        {
            _context = context;
        }

        // GET: Division
        public async Task<IActionResult> Index()
        {
            var hrSystem = _context.Divisions.Include(d => d.Parent);
            return View(await hrSystem.ToListAsync());
        }

        // GET: Division/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions
                .Include(d => d.Parent)
                .FirstOrDefaultAsync(m => m.DivisionId == id);
            if (division == null)
            {
                return NotFound();
            }

            return View(division);
        }

        // GET: Division/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Divisions, "DivisionId", "Name", null);
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DivisionId,Name,ParentId")] Division division)
        {
            if (ModelState.IsValid)
            {
                _context.Add(division);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Divisions, "DivisionId", "Name", null);
            
            return View(division);
        }

        // GET: Division/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions.FindAsync(id);
            if (division == null)
            {
                return NotFound();
            }
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "DivisionId", "Name", division.DivisionId);
            
            return View(division);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DivisionId,Name,ParentId")] Division division)
        {
            if (id != division.DivisionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(division);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DivisionExists(division.DivisionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivisionId"] = new SelectList(_context.Divisions, "DivisionId", "Name", division.DivisionId);
            
            return View(division);
        }

        // GET: Division/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _context.Divisions
                .Include(d => d.Parent)
                .FirstOrDefaultAsync(m => m.DivisionId == id);
            if (division == null)
            {
                return NotFound();
            }

            return View(division);
        }

        // POST: Division/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var division = await _context.Divisions.FindAsync(id);
            _context.Divisions.Remove(division);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool DivisionExists(int id)
        {
            return _context.Divisions.Any(e => e.DivisionId == id);
        }
    }
}
