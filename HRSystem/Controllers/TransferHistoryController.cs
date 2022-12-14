using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRSystem.Services.Interfaces;
using HRSystem.ViewModels;

namespace HRSystem.Controllers
{
    public class TransferHistoryController : Controller
    {
        private readonly ITransferHistoryService _transferHistoryService;

        public TransferHistoryController(ITransferHistoryService transferHistoryService)
        {
            _transferHistoryService = transferHistoryService;
        }
        
        public async Task<IActionResult> Index([FromForm] TransferHistoryViewModel model)
        {
            model.Data = await _transferHistoryService.GetListAsync(model.Filter);

            return View(model);
        }
    }
}
