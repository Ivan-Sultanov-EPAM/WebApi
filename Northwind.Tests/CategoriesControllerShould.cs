using System;
using System.Threading.Tasks;
using FluentAssertions;
using Northwind.Entities;
using Newtonsoft.Json;
using Xunit;
using System.Collections.Generic;
using System.Net;
using Northwind.Application.Models.Requests;
using Northwind.Application.Models.Responses;

namespace Northwind.Tests
{
    public class CategoriesControllerShould : BaseController
    {
        [Fact]
        public async Task Return_All_Categories()
        {
            // Act
            var uri = new Uri("api/categories", UriKind.Relative);
            var getResult = await _apiClient.GetAsync(uri).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var getResponseContent = await getResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var categories = JsonConvert.DeserializeObject<List<Category>>(getResponseContent);

            // Assert
            categories.Should().NotBeNull();
        }

        [Fact]
        public async Task Return_Category_By_Id()
        {
            // Act
            var uri = new Uri("api/categories/1", UriKind.Relative);
            var getResult = await _apiClient.GetAsync(uri).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var getResponseContent = await getResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var categories = JsonConvert.DeserializeObject<CategoryResponseDto>(getResponseContent);

            // Assert
            categories.Should().NotBeNull();
        }

        [Fact]
        public async Task Return_404_If_Category_Does_Not_Exist()
        {
            // Act
            var uri = new Uri("api/categories/0", UriKind.Relative);
            var getResult = await _apiClient.GetAsync(uri).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Return_Ok_For_Edit_Category()
        {
            // Arrange
            var request = new EditCategoryRequestDto
            {
                CategoryName = "Edited Category",
                Description = "Edited Description"
            };

            // Act
            var uri = new Uri("api/categories/1", UriKind.Relative);
            var getResult = await _apiClient.PutAsync(uri, request.ToStringContent()).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Return_404_For_Edit_Category_If_Category_Name_Exceeds_15_Symbols()
        {
            // Arrange
            var request = new EditCategoryRequestDto
            {
                CategoryName = "Edited Category Name",
                Description = "Edited Description"
            };

            // Act
            var uri = new Uri("api/categories/1", UriKind.Relative);
            var getResult = await _apiClient.PutAsync(uri, request.ToStringContent()).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Return_Ok_For_Add_Category()
        {
            // Arrange
            var request = new EditCategoryRequestDto
            {
                CategoryName = "Added Category",
                Description = "Added Description"
            };

            // Act
            var uri = new Uri("api/categories", UriKind.Relative);
            var getResult = await _apiClient.PostAsync(uri, request.ToStringContent()).ConfigureAwait(false);

            // Assert
            getResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Return_Ok_For_Delete_Category()
        {
            // Arrange
            var request = new EditCategoryRequestDto
            {
                CategoryName = "To Delete",
                Description = "Description To Delete"
            };

            // Act
            var postUri = new Uri("api/categories", UriKind.Relative);
            var getPostResult = await _apiClient.PostAsync(postUri, request.ToStringContent()).ConfigureAwait(false);

            // Assert
            getPostResult.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act
            var getResponseContent = await getPostResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var categorytId = JsonConvert.DeserializeObject<int>(getResponseContent);

            // Act
            var uri = new Uri($"api/categories/{categorytId}", UriKind.Relative);
            var getDeleteResult = await _apiClient.DeleteAsync(uri).ConfigureAwait(false);

            // Assert
            getDeleteResult.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
