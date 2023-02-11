using System.Collections.Generic;

namespace Northwind.Models
{
    public class CustomerDemographics
    {
        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }
        public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
    }
}
