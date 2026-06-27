using System;

namespace TakweneMusic.Domain.Entities;

public class Dsp
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

     private Dsp() { }


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
