using Application;
using Infrastructure;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using WebUI.Installers;

namespace Project.WebUI.Installers;

public class CoreInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration Configuration)
    {
        services.AddApplication();
        services.AddIntrastructure(Configuration);

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddRazorPages();
    }
}

