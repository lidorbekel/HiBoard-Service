using HiBoard.Persistence;
using Serilog;

namespace HiBoard.Service;

public static class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        try
        {
            Log.Information("Starting up");
            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<HiBoardDbContext>();
                //context.Database.Migrate();
                var config = host.Services.GetRequiredService<IConfiguration>();
                var seed = new Seed(context);
                seed.SeedData().Wait();
                host.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        catch (Exception ex) {
            Log.Fatal(ex, "Application startup failed");

            throw;
        }
        finally {
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}
