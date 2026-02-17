using HandsOnService.Contracts;
using HandsOnService.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace HandsOnService.Core.Courses;

public class Create: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("courses", static async (CreateCourseRequest request, CancellationToken cancellationToken) =>
        {
            Course course = Course.Create(request.Name, request.Description, request.Author);

            CreateCourseResponse response = new CreateCourseResponse(
                course.Id,
                course.Name,
                course.Description,
                course.Author);

            return "OK";
        });
    }
}