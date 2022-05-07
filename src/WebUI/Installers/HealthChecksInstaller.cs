using Infrastructure.Persistance;
using WebUI.HealthChecks;

namespace WebUI.Installers;

public class HealthChecksInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration Configuration)
    {
        services.AddHealthChecks()
          .AddDbContextCheck<ApplicationDbContext>()
          .AddCheck<CaptachaAPIHealthCheck>("API validating captacha");
    }
}

