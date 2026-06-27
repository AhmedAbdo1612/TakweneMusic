using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Dsps.Common;

namespace TakweneMusic.Application.Dsps.Commands.CreateDsp;

public record CreateDspCommand(string Name) : ICommand<DspDto>;
