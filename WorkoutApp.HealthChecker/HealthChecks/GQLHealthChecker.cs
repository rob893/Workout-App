using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WorkoutApp.HealthChecker.HealthChecks
{
    public class GQLHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory clientFactory;


        public GQLHealthCheck(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var client = clientFactory.CreateClient();

            var res = await client.GetAsync("http://localhost:4000/.well-known/apollo/server-health");

            var healthCheckResultHealthy = res.IsSuccessStatusCode;

            if (healthCheckResultHealthy)
            {
                return HealthCheckResult.Healthy("GQL is running.");
            }

            return HealthCheckResult.Unhealthy("GQL is not running.");
        }
    }
}