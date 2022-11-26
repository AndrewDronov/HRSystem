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
        
        public async Task<IActionResult> Index()
        {
            var hrSystem = _context.Divisions.Include(d => d.Parent);
            
            return View(await hrSystem.ToListAsync());
        }
        
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
        
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Divisions, "DivisionId", "Name");
            
            return View();
        }
        
        [HttpPost]
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
            ViewData["ParentId"] = new SelectList(_context.Divisions, "DivisionId", "Name", division.ParentId);
            
            return View(division);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("DivisionId,Name,ParentId")] Division division)
        {
            if (id != division.DivisionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(division);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Divisions, "DivisionId", "Name", division.ParentId);
            
            return View(division);
        }
        
        [HttpGet]
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
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var division = await _context.Divisions.FindAsync(id);
            
            _context.Divisions.Remove(division);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
