using HiBoard.Application.Repositories;
using HiBoard.Application.Services;
using HiBoard.Persistence;
using HiBoard.Service.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HiBoard.Service.Extensions;

public static class ServiceExtensions
{
    public static void AddMyServices(this IServiceCollection services)
    {
        services.AddScoped<UsersService>();
    }

    public static void AddMyRepositories(this IServiceCollection services)
    {
        services.AddScoped<UsersRepository>();
    }

    public static void AddMyDb(this IServiceCollection services, IConfiguration configuration)
    {
        string mysqlConnectionString = configuration.GetConnectionString("MSSQL");
        services.AddDbContext<HiBoardDbContext>(options =>
            options.UseSqlServer(mysqlConnectionString));
    }

    public static void AddMySwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(SwaggerConfiguration.Configure);
    }

    public static void AddMyCors(this IServiceCollection services, IConfiguration configuration)
    {
        string[]? cors = configuration?.GetSection("Cors")?.GetChildren()?.Select(cors => cors.Value).ToArray();

        services.AddCors(options =>
            options.AddPolicy("CorsPolicy", policy =>
            {
                if (cors != null)
                    policy.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(cors);
            }));
    }

    public static void AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://securetoken.google.com/hiboard-e147b";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/hiboard-e147b",
                    ValidateAudience = true,
                    ValidAudience = "hiboard-e147b",
                    ValidateLifetime = true,
                };
            });
    }
}