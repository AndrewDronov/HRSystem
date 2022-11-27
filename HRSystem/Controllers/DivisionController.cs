using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRSystem.Models;
using HRSystem.Services.Interfaces;
using HRSystem.ViewModels;

namespace HRSystem.Controllers
{
    public class DivisionController : Controller
    {
        private readonly IDivisionService _divisionService;

        public DivisionController(IDivisionService divisionService)
        {
            _divisionService = divisionService;
        }
        
        public async Task<IActionResult> Index([FromForm] DivisionViewModel model)
        {
            model.Data = await _divisionService.GetListAsync(model.Filter);
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _divisionService.GetByIdAsync(id.Value);

            if (division == null)
            {
                return NotFound();
            }

            return View(division);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["ParentId"] = new SelectList(await _divisionService.GetListAsync(), "DivisionId", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Division division)
        {
            if (ModelState.IsValid)
            {
                await _divisionService.CreateAsync(division);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ParentId"] = new SelectList(await _divisionService.GetListAsync(), "DivisionId", "Name", null);

            return View(division);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _divisionService.GetByIdAsync(id.Value);

            if (division == null)
            {
                return NotFound();
            }

            ViewData["ParentId"] = new SelectList(await _divisionService.GetListAsync(), "DivisionId", "Name",
                division.ParentId);

            return View(division);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] Division division)
        {
            if (id != division.DivisionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _divisionService.UpdateAsync(division);

                return RedirectToAction(nameof(Index));
            }

            ViewData["ParentId"] = new SelectList(await _divisionService.GetListAsync(), "DivisionId", "Name",
                division.ParentId);

            return View(division);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _divisionService.GetByIdAsync(id.Value);

            if (division == null)
            {
                return NotFound();
            }

            return View(division);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _divisionService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}