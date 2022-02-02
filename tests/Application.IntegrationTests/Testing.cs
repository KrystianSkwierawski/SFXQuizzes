using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUI;

namespace Application.IntegrationTests;

[SetUpFixture]
public class Testing
{
    private static IConfigurationRoot _configuration;
    private static IServiceScopeFactory _scopeFactory;
    private static Checkpoint _checkpoint;

    private static string? _currentUserId;
    private static string? _currentUserName;


    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

        _configuration = builder.Build();

        var startup = new Startup(_configuration);

        var services = new ServiceCollection();

        Environment.SetEnvironmentVariable("RunningTests", "true");

        services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.EnvironmentName == "Development" &&
            w.ApplicationName == "WebUI"));

        services.AddLogging();

        startup.ConfigureServices(services);

        services.AddTransient(provider =>
           Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId && s.UserName == _currentUserName));

        _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

        _checkpoint = new Checkpoint
        {
            TablesToIgnore = new[] { "__EFMigrationsHistory" }
        };

        EnsureDatabase();
    }

    private static void EnsureDatabase()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        context.Database.Migrate();
    }


    public static async Task<ApplicationUser> CreateUserAsync()
    {
        using var scope = _scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

        ApplicationUser user = new()
        {
            Email = "test@gmail.com",
            UserName = "test",
        };

        var createUserResult = await userManager.CreateAsync(user);

        if (!createUserResult.Succeeded)
        {
            var errors = string.Join(Environment.NewLine, createUserResult.Errors.Select(e => e.Description));

            throw new Exception($"Unable to create {user.UserName}.{Environment.NewLine}{errors}");
        }

        return user;
    }

    public static async Task<(string, string)> RunAsDefaultUserAsync()
    {
        return await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());
    }

    public static async Task<(string, string)> RunAsAdministratorAsync()
    {
        return await RunAsUserAsync("administrator@local", "Administrator1234!", new[] { "Administrator" });
    }

    public static async Task<(string, string)> RunAsUserAsync(string userName, string password, string[] roles)
    {
        using var scope = _scopeFactory.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = new ApplicationUser { UserName = userName, Email = userName };

        var result = await userManager.CreateAsync(user, password);

        if (roles.Any())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            await userManager.AddToRolesAsync(user, roles);
        }

        if (result.Succeeded)
        {
            _currentUserId = user.Id;
            _currentUserName = user.UserName;

            return (_currentUserId, _currentUserName);
        }

        throw new Exception($"Unable to create {userName}.{Environment.NewLine}{result}");
    }


    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task ResetState()
    {
        await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
    }

    public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task<TEntity> AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        await context.AddAsync(entity);

        await context.SaveChangesAsync();

        return entity;
    }

    public static async Task AddRangeAsync<TEntity>(List<TEntity> entities)
      where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        await context.AddRangeAsync(entities);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }
}
