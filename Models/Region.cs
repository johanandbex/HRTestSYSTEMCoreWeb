using System;
using System.Collections.Generic;

#nullable disable

namespace HRSystemCore.Models
{
    public partial class Region
    {
        public Region()
        {
            Companies = new HashSet<Company>();
        }

        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
