using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace HiBoard.Service.Extensions;
    public static class AppExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var result = new
                        {
                            context.Response.StatusCode,
                            contextFeature.Error.Message
                        };

                        string json;
                        try
                        {
                            json = JsonSerializer.Serialize(result);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }
    }
