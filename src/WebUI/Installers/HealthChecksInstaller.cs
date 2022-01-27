using Infrastructure.Persistance;

namespace WebUI.Installers;

public class HealthChecksInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration Configuration)
    {
        {
            services.AddHealthChecks()
              .AddDbContextCheck<ApplicationDbContext>();
        }
    }
}

