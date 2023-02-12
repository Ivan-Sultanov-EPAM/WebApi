using System;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using System.Collections.Generic;
using System.Net;
using Northwind.Application.Models.Requests;
using Northwind.Application.Models.Responses;

namespace Northwind.Tests
{
    public class ProductControllerShould : BaseController
    {
        [Fact]
        public async Task Return_First_10_Products()
        {
            // Act
            var uri = new Uri("api/products", UriKind.Relative);
            var getResult = await _apiClient.GetAsync(uri).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var getResponseContent = await getResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var products = JsonConvert.DeserializeObject<List<ProductResponseDto>>(getResponseContent);

            // Assert
            products.Should().NotBeNull();
            products.Should().HaveCount(10);
        }

        [Fact]
        public async Task Return_First_Page_Of_20_Products()
        {
            // Act
            var uri = new Uri("api/products?pageSize=20", UriKind.Relative);
            var getResult = await _apiClient.GetAsync(uri).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var getResponseContent = await getResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var products = JsonConvert.DeserializeObject<List<ProductResponseDto>>(getResponseContent);

            // Assert
            products.Should().NotBeNull();
            products.Should().HaveCount(20);
        }

        [Fact]
        public async Task Return_Second_Page_Of_15_Products()
        {
            // Act
            var uri = new Uri("api/products?pageNumber=1&pageSize=15", UriKind.Relative);
            var getResult = await _apiClient.GetAsync(uri).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var getResponseContent = await getResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var products = JsonConvert.DeserializeObject<List<ProductResponseDto>>(getResponseContent);

            // Assert
            products.Should().NotBeNull();
            products.Should().HaveCount(15);
        }

        [Fact]
        public async Task Return_Products_With_Category_Id_2()
        {
            // Act
            var uri = new Uri("api/products?pageSize=100&categoryId=2", UriKind.Relative);
            var getResult = await _apiClient.GetAsync(uri).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var getResponseContent = await getResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var products = JsonConvert.DeserializeObject<List<ProductResponseDto>>(getResponseContent);

            // Assert
            products.Should().NotBeNull();
            products.TrueForAll(p => p.CategoryId == 2).Should().BeTrue();
        }

        [Fact]
        public async Task Return_Product_By_Id()
        {
            // Act
            var uri = new Uri("api/products/1", UriKind.Relative);
            var getResult = await _apiClient.GetAsync(uri).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var getResponseContent = await getResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var product = JsonConvert.DeserializeObject<ProductResponseDto>(getResponseContent);

            // Assert
            product.Should().NotBeNull();
        }

        [Fact]
        public async Task Return_404_If_Product_Does_Not_Exist()
        {
            // Act
            var uri = new Uri("api/products/0", UriKind.Relative);
            var getResult = await _apiClient.GetAsync(uri).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Return_Ok_For_Edit_Product()
        {
            // Arrange
            var request = new EditProductRequestDto
            {
                ProductName = "Cake",
                SupplierId = 2,
                CategoryId = 1,
                QuantityPerUnit = "20 boxes x 30 bags",
                UnitPrice = 19,
                UnitsInStock = 39,
                UnitsOnOrder = 3,
                ReorderLevel = 10,
                Discontinued = false
            };

            // Act
            var uri = new Uri("api/products/1", UriKind.Relative);
            var getResult = await _apiClient.PutAsync(uri, request.ToStringContent()).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Return_Ok_For_Add_Product()
        {
            // Arrange
            var request = new EditProductRequestDto
            {
                ProductName = "Salt",
                SupplierId = 2,
                CategoryId = 1,
                QuantityPerUnit = "5 boxes x 10 bags",
                UnitPrice = 10,
                UnitsInStock = 100,
                UnitsOnOrder = 3,
                ReorderLevel = 10,
                Discontinued = false
            };

            // Act
            var uri = new Uri("api/products", UriKind.Relative);
            var getResult = await _apiClient.PostAsync(uri, request.ToStringContent()).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Return_Ok_For_Delete_Product()
        {
            // Arrange
            var request = new EditProductRequestDto
            {
                ProductName = "Salt",
                SupplierId = 2,
                CategoryId = 1,
                QuantityPerUnit = "5 boxes x 10 bags",
                UnitPrice = 10,
                UnitsInStock = 100,
                UnitsOnOrder = 3,
                ReorderLevel = 10,
                Discontinued = false
            };

            // Act
            var postUri = new Uri("api/products", UriKind.Relative);
            var getPostResult = await _apiClient.PostAsync(postUri, request.ToStringContent()).ConfigureAwait(false);

            // Assert
            getPostResult.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var getResponseContent = await getPostResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var productId = JsonConvert.DeserializeObject<int>(getResponseContent);

            // Act
            var uri = new Uri($"api/products/{productId}", UriKind.Relative);
            var getDeleteResult = await _apiClient.DeleteAsync(uri).ConfigureAwait(false);

            // Assert
            getDeleteResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
