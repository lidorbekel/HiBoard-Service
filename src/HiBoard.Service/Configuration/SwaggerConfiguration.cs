using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HiBoard.Service.Configuration;

[ExcludeFromCodeCoverage]
public static class SwaggerConfiguration
{
    public static void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Hi Board API", Version = "v1" });

        options.TagActionsBy(api =>
        {
            if (api.GroupName != null) return new[] { api.GroupName };

            if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                return new[] { controllerActionDescriptor.ControllerName };

            throw new InvalidOperationException("Unable to determine tag for endpoint.");
        });
        options.EnableAnnotations();

        const string jwtSecurityDefinitionName = "jwt_auth";
        options.AddSecurityDefinition(jwtSecurityDefinitionName, new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Name = "Authorization",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = jwtSecurityDefinitionName,
                    },
                },
                Array.Empty<string>()
            },
        });

    }
}
