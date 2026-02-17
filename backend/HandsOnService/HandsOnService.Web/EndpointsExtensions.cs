using System.Reflection;
using HandsOnService.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HandsOnService.Web;

public static class EndpointsExtensions
{
    /*
    Reflection allows us to dynamically examine code at runtime.
    For Minimal API registration, we'll use reflection to scan our .NET assemblies and find classes that implement IEndpoint.
    /// Then, we will configure them as services with dependency injection.

    The Assembly parameter should be the assembly that contains the IEndpoint implementations.
    If you want to have endpoints in multiple assemblies (projects), you can easily extend this method to accept a collection.
    */
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    /*
     The final step in our implementation is to register the endpoints automatically.
     We can create an extension method on the WebApplication, which lets us resolve services using the IServiceProvider.

    We're looking for all registrations of the IEndpoint service.
    These will be the endpoint classes we can now register with the application by calling MapEndpoint.

    I'm also adding an option to pass in a RouteGroupBuilder if you want to apply conventions to all endpoints.
    A great example is adding a route prefix, authentication, or API versioning.
     */
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.Services
            .GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder =
            routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        app.Logger.LogInformation($"IEndpoint count = {endpoints.Count()}");
        foreach (var e in endpoints)
            app.Logger.LogInformation(e.GetType().FullName);


        return app;
    }
}