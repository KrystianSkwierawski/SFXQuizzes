using Application.Common.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Linq;


namespace WebUI.HealthChecks;

public class CaptachaAPIHealthCheck : IHealthCheck
{
    private readonly ICaptachaAPIService _captachaAPIService;

    public CaptachaAPIHealthCheck(ICaptachaAPIService captachaAPIService)
    {
        _captachaAPIService = captachaAPIService;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        JObject response = await _captachaAPIService.GetResponseAsync(null);

        bool healthy = (response["success"]?.Value<bool>() == false);

        if (healthy)
            return HealthCheckResult.Healthy(response.ToString());

        return HealthCheckResult.Unhealthy(response.ToString());
    }
}

