using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HiBoard.Service.Configuration;

public static class SwaggerConfiguration
{
    public static void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "HiBoard API", Version = "v1" });

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
