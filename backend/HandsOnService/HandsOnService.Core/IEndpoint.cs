using Microsoft.AspNetCore.Routing;

namespace HandsOnService.Core;

/// <summary>
/// The MapEndpoint accepts an IEndpointRouteBuilder, which we can use to call MapGet, MapPost, etc.
/// </summary>
public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder builder);
}