using HandsOnService.Core;

namespace HandsOnService.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddEndpoints(typeof(IEndpoint).Assembly);

        return services;
    }
}