using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HandsOnService.AppHost;

internal static class ResourceBuilderExtensions
{
    public static IResourceBuilder<T> WithSwaggerUI<T>(this IResourceBuilder<T> builder)
        where T : IResourceWithEndpoints
    {
        return builder.WithOpenApiDocs("swagger-ui-docs", "Swagger API Documentation", "index.html");
    }

    public static IResourceBuilder<T> WithScalar<T>(this IResourceBuilder<T> builder)
        where T : IResourceWithEndpoints
    {
        return builder.WithOpenApiDocs("scalar-docs", "Scalar API Documentation", "scalar/v1");
    }

    public static IResourceBuilder<T> WithReDoc<T>(this IResourceBuilder<T> builder)
        where T : IResourceWithEndpoints
    {
        return builder.WithOpenApiDocs("redoc-docs", "ReDoc API Documentation", "api-docs/index.html");
    }

    private static IResourceBuilder<T> WithOpenApiDocs<T>(
        this IResourceBuilder<T> builder,
        string name,
        string displayName,
        string openApiUIPath)
        where T: IResourceWithEndpoints
    {
        builder.WithCommand(
            name,
            displayName,
            executeCommand: async _ =>
            {
                try
                {
                    // Base URL for API
                    EndpointReference endpoint = builder.GetEndpoint("https");

                    string url = $"{endpoint.Url}/{openApiUIPath}";

                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

                    return new ExecuteCommandResult { Success = true };
                }
                catch (Exception e)
                {
                    return new ExecuteCommandResult
                    {
                        Success = false,
                        ErrorMessage = e.ToString()
                    };
                }
            },
            new CommandOptions()
            {
                IconName = "Document",
                IconVariant = IconVariant.Filled,
                UpdateState = context => context.ResourceSnapshot.HealthStatus == HealthStatus.Healthy ?
                            ResourceCommandState.Enabled : ResourceCommandState.Disabled
            });

        return builder;
    }
}