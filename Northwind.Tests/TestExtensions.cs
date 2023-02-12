using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Northwind.Tests
{
    public static class TestExtensions
    {
        public static StringContent ToStringContent(this object request)
        {
            return new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        }
    }
}