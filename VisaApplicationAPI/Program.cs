using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.Tasks;
using VisaApplication.Model.Security;
using VisaApplication.Seeder.IdentitySeed;
using VisaApplication.SqlServer.DataBase;
using VisaApplicationSharedUI.Configuration;
using VisaApplicationSharedUI.Controller.ExceptionHandler;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.StartServiceConfiguration()
                .AddDbContextService<VisaApplicationDbContext>(
                builder.Configuration.GetConnectionString("DevelopmentConnection"))
                .AddIdentityService<
                    VisaApplicationDbContext, VisaApplicationUser, VisaApplicationRole>()
                .RepositoriesRegisteration()
                .ServicesRegisteration()
                .AddAuthenticationService();

builder.Services.AddTransient<ExceptionHandling>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = false;
                    options.RequireHttpsMetadata = false;
                    options.Authority = builder.Configuration["JWT:Issuer"];
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Visa Application API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT Bearer token **_only_**",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});


var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var dbcontext = services.GetRequiredService<VisaApplicationDbContext>();
    dbcontext.Database.Migrate();
}

await Seeder.SeedDataAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
         .SetIsOriginAllowed(origin => true)
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials());

app.RegisterExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }