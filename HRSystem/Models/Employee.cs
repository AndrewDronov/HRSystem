using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace HRSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        
        [DisplayName("Имя")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Отчество")]
        public string MiddleName { get; set; }
        
        [Required]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        
        [DisplayName("Подразделение")]
        public int? DivisionId { get; set; }

        [DisplayName("Подразделение")]
        public virtual Division Division { get; set; }
    }
}
