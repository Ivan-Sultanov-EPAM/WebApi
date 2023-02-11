using System.Collections.Generic;

namespace Northwind.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        public string RegionDescription { get; set; }
        public virtual ICollection<Territories> Territories { get; set; }
    }
}
