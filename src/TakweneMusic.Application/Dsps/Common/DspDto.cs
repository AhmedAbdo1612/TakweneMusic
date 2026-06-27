using System;
using TakweneMusic.Domain.Entities;

namespace TakweneMusic.Application.Dsps.Common;

public class DspDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static DspDto FromEntity(Dsp dsp)
    {
        return new DspDto
        {
            Id = dsp.Id,
            Name = dsp.Name
        };
    }
}
