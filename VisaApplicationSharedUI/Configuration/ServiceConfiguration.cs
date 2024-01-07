
//using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VisaApplication.Model.Security;
using VisaApplication.Service.Repositories.Applicant.Data;
using VisaApplication.Service.Repositories.Applicant.IData;
using VisaApplication.Service.Repositories.Content.Data;
using VisaApplication.Service.Repositories.Content.IData;
using VisaApplication.Service.Repositories.Security.Data;
using VisaApplication.Service.Repositories.Security.IData;
using VisaApplication.Service.Repositories.VisaType.Data;
using VisaApplication.Service.Repositories.VisaType.IData;
using VisaApplication.Service.Services.Applicant.Data;
using VisaApplication.Service.Services.Applicant.IData;
using VisaApplication.Service.Services.Content.Data;
using VisaApplication.Service.Services.Content.IData;
using VisaApplication.Service.Services.Security.Data;
using VisaApplication.Service.Services.Security.IData;
using VisaApplication.Service.Services.VisaType.Data;
using VisaApplication.Service.Services.VisaType.IData;
using VisaApplication.SqlServer.DataBase;
using VisaApplicationSharedUI.Controller.ExceptionHandler;

namespace VisaApplicationSharedUI.Configuration;
public static class ServiceConfiguration
{
    public static IServiceCollection StartServiceConfiguration(
        this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection RepositoriesRegisteration(
        this IServiceCollection services)
    {
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IVisaTypeRepository, VisaTypeRepository>();
        services.AddTransient<IApplicantRepository, ApplicantRepository>();
        services.AddTransient<IFileRepository, FileRepository>();

        return services;
    }

    public static IServiceCollection ServicesRegisteration(
        this IServiceCollection services)
    {
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IVisaTypeService, VisaTypeService>();
        services.AddTransient<IApplicantService, ApplicantService>();
        services.AddTransient<IFileService, FileService>();

        return services;
    }

    public static IServiceCollection AddDbContextService<TDbContext>(this IServiceCollection services, string connectionString)
             where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(options =>
                     options.UseSqlServer(connectionString));

        return services;
    }

    public static IServiceCollection AddIdentityService<TDbContext, 
        IIdentityUser, IIdentityRole>(
        this IServiceCollection services)
          where TDbContext : VisaApplicationDbContext
          where IIdentityUser : VisaApplicationUser
          where IIdentityRole : VisaApplicationRole
    {
        services.AddIdentity<IIdentityUser, IIdentityRole>()
      .AddEntityFrameworkStores<TDbContext>()
      .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddAuthenticationService(
        this IServiceCollection services, 
        Action<IdentityOptions> configureOptions = null)
    {
        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        if (configureOptions is null)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = false;
            });

            return services;
        }

        services.Configure<IdentityOptions>(configureOptions);
        return services;
    }

    public static void RegisterExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandling>();
    }
}
