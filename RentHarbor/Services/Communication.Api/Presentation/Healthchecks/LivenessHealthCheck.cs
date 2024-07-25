using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Communication.Api.Healthchecks
{
    public class LivenessHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isLive = true;

            if (isLive)
            {
                return Task.FromResult(HealthCheckResult.Healthy("The application is live."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("The application is not live."));
        }
    }
}

