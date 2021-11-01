using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HRSystemCore.Models
{
    public partial class Status
    {
        public Status()
        {
            People = new HashSet<Person>();
        }

        public int StatusId { get; set; }
        [Column("Status")]
        public string Status1 { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
