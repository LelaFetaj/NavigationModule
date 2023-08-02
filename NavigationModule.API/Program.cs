using System.Net;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using NetLoggings.Extensions;
using Serilog;
using NavigationModule.Extensions;

namespace NavigationModule.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllerWithFilters();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCustomAuthentication(builder.Configuration);
        builder.Services.AddSwagger();
        builder.Services.AddAuthenticationContext();
        builder.Services.AddAuthenticationBrokers();
        builder.Services.AddAuthenticationServices();
        builder.Services.AddCustomLogging();
        builder.Services.AddHealthChecks()
            .AddCustomHealthChecks(builder.Configuration);
        builder.Configuration.AddEnvironmentVariables();

        builder.Host.UseSerilog(SeriLogger.Configure);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerService();
        }

        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.MapHealthChecks(pattern: "/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
        else
        {
            app.MapHealthChecks(pattern: "/health");
        }

        app.Run();
    }
}

