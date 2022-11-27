using HRSystem.Filters;
using HRSystem.Models;
using System.Collections.Generic;

namespace HRSystem.ViewModels
{
    public class TransferHistoryViewModel
    {
        public TransferHistoryFilter Filter { get; set; }
        
        public List<TransferHistory> Data { get; set; }
    }
}