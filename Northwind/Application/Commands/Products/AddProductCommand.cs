using MediatR;
using Northwind.Application.Models.Requests;

namespace Northwind.Application.Commands.Products
{
    public class AddProductCommand : IRequest<int>
    {
        public string ProductName { get; }
        public int? SupplierId { get; }
        public int? CategoryId { get; }
        public string QuantityPerUnit { get; }
        public decimal? UnitPrice { get; }
        public short? UnitsInStock { get; }
        public short? UnitsOnOrder { get; }
        public short? ReorderLevel { get; }
        public bool Discontinued { get; }

        public AddProductCommand(AddProductRequestDto request)
        {
            ProductName = request.ProductName;
            SupplierId = request.SupplierId;
            CategoryId = request.CategoryId;
            QuantityPerUnit = request.QuantityPerUnit;
            UnitPrice = request.UnitPrice;
            UnitsInStock = request.UnitsInStock;
            UnitsOnOrder = request.UnitsOnOrder;
            ReorderLevel = request.ReorderLevel;
            Discontinued = request.Discontinued;
        }
    }
}