using System;
using System.Collections.Generic;

#nullable disable

namespace HRSystemCore.Models
{
    public partial class Company
    {
        public Company()
        {
            Departments = new HashSet<Department>();
            People = new HashSet<Person>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
