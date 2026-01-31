namespace HandsOnService.Contracts;

public sealed record CreateCourseRequest(string Name, string Description, string Author);