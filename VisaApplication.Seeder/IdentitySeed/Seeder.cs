
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VisaApplication.Model.Security;
using VisaApplication.Seeder.IdentitySeed.Data;
using VisaApplication.Seeder.IdentitySeed.IData;
using VisaApplication.Seeder.VisaTypeSeed.Data;
using VisaApplication.Seeder.VisaTypeSeed.IData;
using VisaApplication.SqlServer.DataBase;

namespace VisaApplication.Seeder.IdentitySeed;
public static class Seeder
{
    public static ServiceCollection Services { get; set; }

    private static void ConfigureServices()
    {
        Services = new ServiceCollection();

        Services.AddDbContext<VisaApplicationDbContext>(
        options =>
        {
            options.UseSqlServer(
                "Data Source=DESKTOP-73QCJ9H\\PROGRAMMER;Initial Catalog=VisaApplicationDB;Integrated Security=True;Encrypt=false;TrustServerCertificate=true");
        });

        Services.AddIdentity<VisaApplicationUser, VisaApplicationRole>()
            .AddEntityFrameworkStores<VisaApplicationDbContext>()
            .AddDefaultTokenProviders();
        Services.AddScoped<IUserCreationService, UserCreationService>();
        Services.AddLogging(config =>
        {

        });
        Services.AddScoped<IRoleCreationService, RoleCreationService>();
        Services.AddScoped<IVisaTypeCreationService, VisaTypeCreationService>();
    }

    public async static Task EnsureDatabaseExistAsync(
        VisaApplicationDbContext context)
    {
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await context.Database.MigrateAsync();
        }
    }

    public async static Task<bool> SeedDataAsync()
    {
        ConfigureServices();

        var provider = Services.BuildServiceProvider();
        var context = provider.GetService<VisaApplicationDbContext>();

        await EnsureDatabaseExistAsync(context);

        using (var transaction = await context.Database.BeginTransactionAsync())
        {
            try
            {
                await SeedRoleAsync(provider);

                if (!context.Users.Any())
                {
                    await SeedUserAsync(provider);
                }

                if (!context.VisaTypes.Any())
                {
                    await SeedVisaTypesAsync(provider);
                }

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }

    private async static Task SeedRoleAsync(ServiceProvider serviceProvider)
    {
        var roleService = serviceProvider.GetService<IRoleCreationService>();
        await roleService.CreateRolesAsync();
    }

    private async static Task SeedUserAsync(ServiceProvider serviceProvider)
    {
        var userService = serviceProvider.GetService<IUserCreationService>();
        await userService.CreateUsersAsync();
    }

    private async static Task SeedVisaTypesAsync(ServiceProvider serviceProvider)
    {
        var visaTypeService = serviceProvider.GetService<IVisaTypeCreationService>();
        await visaTypeService.CreateVisaTypesAsync();
    }
}
