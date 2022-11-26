using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace HRSystem.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }
        
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        
        [DisplayName("Подразделение")]
        public int? DivisionId { get; set; }

        [DisplayName("Подразделение")]
        public virtual Division Division { get; set; }
    }
}
