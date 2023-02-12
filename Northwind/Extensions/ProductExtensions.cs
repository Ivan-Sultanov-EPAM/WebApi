using Northwind.Application.Commands.Products;
using Northwind.Application.Models.Responses;
using Northwind.Entities;

namespace Northwind.Extensions
{
    public static class ProductExtensions
    {
        public static ProductResponseDto ToProductResponseDto(this Product product)
        {
            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued
            };
        }

        public static void UpdateProduct(this Product product, EditProductCommand request)
        {
            product.ProductName = request.ProductName;
            product.SupplierId = request.SupplierId;
            product.CategoryId = request.CategoryId;
            product.QuantityPerUnit = request.QuantityPerUnit;
            product.UnitPrice = request.UnitPrice;
            product.UnitsInStock = request.UnitsInStock;
            product.UnitsOnOrder = request.UnitsOnOrder;
            product.ReorderLevel = request.ReorderLevel;
            product.Discontinued = request.Discontinued;
        }
    }
}