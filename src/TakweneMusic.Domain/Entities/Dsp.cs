using System;

namespace TakweneMusic.Domain.Entities;

public class Dsp
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    // Parameterless constructor for serialization / EF Core compatibility
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Dsp() { }
#pragma warning restore CS8618

    public Dsp(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public void Update(string name)
    {
        Name = name;
    }
}
