using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OpenTelemetry.Shared;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddOpenTelemetryExt(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OpenTelemetryConstants>(
            configuration.GetSection("OpenTelemetry"));

        var openTelemetryConstants =
            configuration.GetSection("OpenTelemetry")
                         .Get<OpenTelemetryConstants>()!;

        ActivitySourceProvider.Source =
            new System.Diagnostics.ActivitySource(
                openTelemetryConstants.ActivitySourceName);

        services.AddOpenTelemetry()
            .WithTracing(options =>
            {
                options
                    .AddSource(openTelemetryConstants.ActivitySourceName)
                    .ConfigureResource(resource =>
                    {
                        resource.AddService(
                            openTelemetryConstants.ServiceName,
                            serviceVersion: openTelemetryConstants.ServiceVersion);
                    });
            });

        return services;
    }
}