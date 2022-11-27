using HRSystem.Models;
using System;
using System.ComponentModel;

namespace HRSystem.Filters
{
    public class DivisionFilter
    {
        [DisplayName("Дата создания")]
        [DefaultValue(true)]
        public DateTime CreatedAt { get; set; }
    }
}