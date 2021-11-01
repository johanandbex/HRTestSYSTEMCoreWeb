using System;
using System.Collections.Generic;

#nullable disable

namespace HRSystemCore.Models
{
    public partial class Department
    {
        public Department()
        {
            People = new HashSet<Person>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
