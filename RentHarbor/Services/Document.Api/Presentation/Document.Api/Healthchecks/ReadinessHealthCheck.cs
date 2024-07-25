using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Document.Api.Healthchecks
{
    public class ReadinessHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isReady = true;

            if (isReady)
            {
                return Task.FromResult(HealthCheckResult.Healthy("The application is ready."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("The application is not ready."));
        }
    }
}

