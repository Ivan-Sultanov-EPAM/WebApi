using System.Collections.Generic;

namespace Northwind.Entities
{
    public class Shippers
    {
        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
