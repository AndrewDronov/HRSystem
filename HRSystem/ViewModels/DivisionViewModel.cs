using HRSystem.Filters;
using HRSystem.Models;
using System.Collections.Generic;

namespace HRSystem.ViewModels
{
    public class DivisionViewModel
    {
        public DivisionFilter Filter { get; set; }
        
        public List<Division> Data { get; set; }
    }
}