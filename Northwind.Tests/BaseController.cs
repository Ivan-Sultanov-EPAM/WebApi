using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;


namespace Northwind.Tests
{
    public abstract class BaseController
    {
        protected readonly HttpClient _apiClient;

        protected BaseController()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _apiClient = webAppFactory.CreateDefaultClient();
        }
    }
}