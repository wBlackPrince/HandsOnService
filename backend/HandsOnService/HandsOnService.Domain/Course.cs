using System.Runtime.InteropServices.ComTypes;

namespace HandsOnService.Domain;

public class Course
{
    private List<Module> _modules = [];

    // Ef Core
    private Course() { }

    private Course(string name, string description, string author)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Author = author;
    }

    public static Course Create(string name, string description, string author)
    {
        return new(name, description, author);
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public string Author { get; private set; } = string.Empty;

    public IReadOnlyCollection<Module> Modules => _modules;


    public void AddModule(Module module) => _modules.Add(module);
}

