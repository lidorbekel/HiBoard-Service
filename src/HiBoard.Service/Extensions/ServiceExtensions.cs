using System.Text.Json.Serialization;
using HiBoard.Application.Mapping;
using HiBoard.Application.Repositories;
using HiBoard.Application.Services;
using HiBoard.Persistence;
using HiBoard.Service.Configuration;
using HiBoard.Service.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HiBoard.Service.Extensions;

public static class ServiceExtensions
{
    public static void AddMyServices(this IServiceCollection services)
    {
        services.AddScoped<UsersService>();
        services.AddScoped<ActivitiesService>();
        services.AddScoped<CompaniesService>();
        services.AddScoped<UserActivitiesService>();
        services.AddScoped<TemplatesService>();
    }

    public static void AddMyRepositories(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<UsersRepository>();
        services.AddScoped<ActivitiesRepository>();
        services.AddScoped<CompaniesRepository>();
        services.AddScoped<UserActivitiesRepository>();
        services.AddScoped<TemplatesRepository>();
    }

    public static void AddMyDb(this IServiceCollection services, IConfiguration configuration)
    {
        var mysqlConnectionString = configuration.GetConnectionString("Colman");
        //var mysqlConnectionString = configuration.GetConnectionString("MSSQL");
        services.AddDbContext<HiBoardDbContext>(options =>
        {
            options.UseSqlServer(mysqlConnectionString);
            options.EnableSensitiveDataLogging();
        });
    }

    public static void AddMySwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(SwaggerConfiguration.Configure);
    }

    public static void AddMyCors(this IServiceCollection services, IConfiguration configuration)
    {
        var cors = configuration.GetSection("Cors")?.GetChildren()?.Select(cors => cors.Value).ToArray();

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
                    ValidateLifetime = false
                };
            });
    }

    public static void AddMyMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(GeneralProfile).Assembly);
    }

    public static void AddMyKestrel(this IServiceCollection services, IConfiguration configuration)
    {
        var kestrel = configuration?.GetSection("Kestrel");
        services.Configure<KestrelServerOptions>(options =>
        {
            if (kestrel != null)
            {
                options.Configure(kestrel);
            }
        });
    }

    public static void AddMySerializerSettings(this IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.Converters.Add(new JsonTimeSpanConverter());
        });
    }
}