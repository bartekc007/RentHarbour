using System.Net;
using System.Threading.Tasks;
using Basket.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Basket.Integration.Tests
{
    public class HealthCheckTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public HealthCheckTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/health/readiness")]
        [InlineData("/health/live")]
        public async Task HealthChecks_ShouldReturnOk(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

