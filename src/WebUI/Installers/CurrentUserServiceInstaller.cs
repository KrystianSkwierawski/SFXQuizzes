using Application.Common.Interfaces;
using WebUI.Installers;
using WebUI.Services;

namespace Project.WebUI.Installers;

public class CurrentUserServiceInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration Configuration)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
    }
}

