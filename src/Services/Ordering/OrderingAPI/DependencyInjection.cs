using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;

namespace OrderingAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!);
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler(options => { });
        app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        return app;
    }

}
