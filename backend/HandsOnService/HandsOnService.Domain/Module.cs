namespace HandsOnService.Domain;

public record Module
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;
}