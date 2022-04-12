using Application;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using WebMarkupMin.AspNetCore6;
using WebMarkupMin.Core;
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
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();


        // HTML minification (https://github.com/Taritsyn/WebMarkupMin)
        services
            .AddWebMarkupMin(
                options =>
                {
                    options.AllowMinificationInDevelopmentEnvironment = true;
                    options.DisablePoweredByHttpHeaders = true;
                })
            .AddHtmlMinification(
                options =>
                {
                    options.MinificationSettings.RemoveOptionalEndTags = false;
                    options.MinificationSettings.WhitespaceMinificationMode = WhitespaceMinificationMode.Safe;
                });

        // Bundling, minification and Sass transpilation (https://github.com/ligershark/WebOptimizer)
        services.AddWebOptimizer(
            pipeline =>
            {
                pipeline.MinifyJsFiles();
                pipeline.CompileScssFiles()
                        .InlineImages(1);
            });

        services.AddRazorPages();
    }
}

