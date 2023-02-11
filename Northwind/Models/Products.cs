using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Models
{
    public class Products
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(25)]
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        
        [Required]
        [StringLength(25)]
        public string QuantityPerUnit { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal? UnitPrice { get; set; }

        [Required]
        [Range(0, short.MaxValue)]
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
