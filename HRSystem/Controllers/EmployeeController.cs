using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRSystem.Models;
using HRSystem.Services.Interfaces;

namespace HRSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDivisionService _divisionService;

        public EmployeeController(IEmployeeService employeeService, IDivisionService divisionService)
        {
            _employeeService = employeeService;
            _divisionService = divisionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _employeeService.GetListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetByIdAsync(id.Value);
            
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["DivisionId"] = new SelectList(await _divisionService.GetListAsync(), "DivisionId", "Name", null);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivisionId"] = new SelectList(await _divisionService.GetListAsync(), "DivisionId", "Name", null);
            
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetByIdAsync(id.Value);
            
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DivisionId"] = new SelectList(await _divisionService.GetListAsync(), "DivisionId", "Name", employee.DivisionId);
            
            return View(employee);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               await _employeeService.UpdateAsync(employee);
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["DivisionId"] = new SelectList(await _divisionService.GetListAsync(), "DivisionId", "Name", employee.DivisionId);
            
            return View(employee);
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetByIdAsync(id.Value);
            
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _employeeService.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
