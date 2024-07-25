using System.Net;
using Document.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Document.Integration.Tests
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

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

