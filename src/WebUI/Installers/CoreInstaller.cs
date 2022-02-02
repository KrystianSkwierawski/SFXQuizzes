using Application;
using Infrastructure;
using Infrastructure.Identity;
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

        services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddRazorPages();
    }
}

