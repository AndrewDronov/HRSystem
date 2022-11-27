using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Models
{
    public partial class TransferHistory
    {
        [Key]
        public int TransferId { get; set; }

        [DisplayName("ID сотрудника")]
        public int EmployeeId { get; set; }
        [DisplayName("ID подразделения")]
        public int DivisionId { get; set; }
        [DisplayName("Дата зачисления")]
        public DateTime CreatedAt { get; set; }
        
        [DisplayName("Подразделение")]
        public virtual Division Division { get; set; }
        
        [DisplayName("Сотрудник")]
        public virtual Employee Employee { get; set; }
    }
}