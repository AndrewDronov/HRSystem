using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace HRSystem.Models
{
    public partial class Division
    {
        public Division()
        {
            Employees = new HashSet<Employee>();
        }

        public int DivisionId { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Родительское подразделение")]
        public int? ParentId { get; set; }
        [DisplayName("Родительское подразделение")]
        public virtual Division Parent { get; set; }

        [DisplayName("Дочерние подразделения")]
        public virtual ICollection<Division> Children { get; set; }
        [DisplayName("Сотрудники")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
