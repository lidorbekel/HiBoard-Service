using HiBoard.Service.Configuration;
using HiBoard.Service.Data;
using HiBoard.Service.Mapping;
using JsonApiDotNetCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Service;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _environment;

    public Startup(IConfiguration configuration, IHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string mysqlConnectionString = _configuration.GetConnectionString("MySql");

        var serverVersion = new MySqlServerVersion(new Version(5, 7));
        services.AddDbContextPool<HiBoardDbContext>(options =>
            options.UseMySql(mysqlConnectionString, serverVersion));

        services
            .AddAutoMapper(typeof(ContactsMapperProfile))
            .AddJsonApi<HiBoardDbContext>(
                options => JsonApiConfiguration.Options(options, _environment),
                JsonApiConfiguration.Discovery,
                JsonApiConfiguration.Resources);

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
