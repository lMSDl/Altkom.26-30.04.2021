using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiInternal
{
    public class RandomHealth : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var second = DateTime.UtcNow.Second;

            if (second % 2 == 0)
            {
                return Task.FromResult(HealthCheckResult.Healthy("I.m OK!", new Dictionary<string, object>() { { "key", 1 }, { "key2", "value" } }));
            }

            if (second % 3 == 0)
            {
                return Task.FromResult(HealthCheckResult.Degraded());
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("DEAD"));
        }
    }
}
