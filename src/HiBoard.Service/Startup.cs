using HiBoard.Service.Configuration;
using HiBoard.Service.Data;
using JsonApiDotNetCore.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HiBoard.Service;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

        string mysqlConnectionString = _configuration.GetConnectionString("MySql");
        services.AddDbContext<HiBoardDbContext>(options =>
            options.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString),
                static dbContextOptions => dbContextOptions.EnableRetryOnFailure(3)));

        services.AddControllers();

        services.AddSwaggerGen(SwaggerConfiguration.Configure);


        services
            .AddHealthChecks()
            .AddMySql(mysqlConnectionString, "MySql hi_board");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseRouting();

        app.UseJsonApi();

        app
            .UseAuthentication()
            .UseAuthorization();

        app.UseEndpoints(static endpoints => endpoints.MapControllers().RequireAuthorization());

        app
            .UseSwagger()
            .UseSwaggerUI(static c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HiBoard API V1"));
    }
}
