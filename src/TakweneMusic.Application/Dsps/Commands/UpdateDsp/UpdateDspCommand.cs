using System;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;
using TakweneMusic.Application.Dsps.Common;

namespace TakweneMusic.Application.Dsps.Commands.UpdateDsp;

public record UpdateDspCommand(Guid Id, string Name) : ICommand<DspDto>;
