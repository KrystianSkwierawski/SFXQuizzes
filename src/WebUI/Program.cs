using Project.WebUI.Installers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistance;


var builder = WebApplication.CreateBuilder(args);


builder.Services.InstallServicesInAssembly(
        builder.Configuration
    );


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

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


app.Run();
