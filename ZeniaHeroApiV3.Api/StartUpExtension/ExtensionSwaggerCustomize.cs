using Microsoft.OpenApi.Models;

namespace ZeniaHeroApiV3.Api.StartUpExtension
{
    public static class ExtensionSwaggerCustomize
    {
        public static void AddSwaggerCustomize(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ZeniaHeroApi", Version = "v3" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Authorization Here!!",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
            },
            new string[] {}
        }
    });
            });
        }
    }
}
