using HiBoard.Service;
using Serilog;


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

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(static webBuilder => webBuilder.UseStartup<Startup>());
