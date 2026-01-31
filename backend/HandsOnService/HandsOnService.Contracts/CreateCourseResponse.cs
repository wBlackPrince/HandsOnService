namespace HandsOnService.Contracts;

public sealed record CreateCourseResponse(Guid Id, string Name, string Description, string Author);