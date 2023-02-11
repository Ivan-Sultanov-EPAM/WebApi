using System.Collections.Generic;

namespace Northwind.Models
{
    public class Categories
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
