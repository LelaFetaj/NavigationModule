using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NetXceptions.ExceptionHandling;
using NavigationModule.API.Infrastructure.Providers;

namespace NavigationModule.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddControllerWithFilters(this IServiceCollection services)
        {
            var externalControllerFeatureProvider = new ExternalControllerFeatureProvider();

            // Add the custom provider to the ApplicationPartManager
            services.Configure<MvcOptions>(options =>
            {
                options.Conventions.Add(new ExternalControllerFeatureProvider());
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = "NavigationModule.Api",
                    Version = "v1",
                };

                options.SwaggerDoc(
                    name: "v1",
                    info: openApiInfo);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT token that is issued after login"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void UseSwaggerService(this IApplicationBuilder builder)
        {
            builder.UseSwagger();

            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: $"./v1/swagger.json",
                    name: "NavigationModule.Api v1");
            });
        }
    }
}