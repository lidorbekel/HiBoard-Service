using HiBoard.Service.Extensions;

namespace HiBoard.Service;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) => _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMyKestrel(_configuration);
        services.AddMyServices();
        services.AddMyRepositories();
        services.AddMySwagger();
        services.AddMyMapper();
        services.AddMyDb(_configuration);
        services.AddMyAuthentication(_configuration);
        services.AddMyCors(_configuration);
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.ConfigureExceptionHandler();
        }

        app.UseRouting();
        app
            .UseAuthentication()
            .UseAuthorization();
        app.UseCors("CorsPolicy");
        app.UseEndpoints(static endpoints => endpoints.MapControllers().RequireAuthorization());

        app
            .UseSwagger()
            .UseSwaggerUI(static c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HiBoard API V1"));
    }
}
