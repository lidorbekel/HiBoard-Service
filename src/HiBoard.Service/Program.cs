using Serilog;

namespace HiBoard.Service;

public static class Program
{
    public static void Main(string[] args)
    {
        try {
            Log.Information("Starting up");
            CreateHostBuilder(args).Build().Run();
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
