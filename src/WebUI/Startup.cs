using Project.WebUI.Installers;
using WebMarkupMin.AspNetCore6;

namespace WebUI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.InstallServicesInAssembly(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }


        app.UseWebMarkupMin();
        app.UseWebOptimizer();

        app.UseStaticFiles(new StaticFileOptions()
        {
            HttpsCompression = Microsoft.AspNetCore.Http.Features.HttpsCompressionMode.Compress,
            OnPrepareResponse = (context) =>
            {
                var headers = context.Context.Response.GetTypedHeaders();
                headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = TimeSpan.FromDays(30)
                };
            }
        });


        app.UseHealthChecks("/health");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{area=App}/{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }
}

