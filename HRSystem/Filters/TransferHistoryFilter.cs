using HRSystem.Models;
using System;
using System.ComponentModel;

namespace HRSystem.Filters
{
    public class TransferHistoryFilter
    {
        [DisplayName("ID сотрудника")]
        public int? EmployeeId { get; set; }
        [DisplayName("ID подразделения")]
        public int? DivisionId { get; set; }
        
        [DisplayName("Дата начала")]
        [DefaultValue(true)]
        public DateTime DateFrom { get; set; }
        
        [DisplayName("Дата окончания")]
        [DefaultValue(true)]
        public DateTime DateTo { get; set; }
        
        [DisplayName("Подразделение")]
        public virtual Division Division { get; set; }
        
        [DisplayName("Сотрудник")]
        public virtual Employee Employee { get; set; }
    }
}