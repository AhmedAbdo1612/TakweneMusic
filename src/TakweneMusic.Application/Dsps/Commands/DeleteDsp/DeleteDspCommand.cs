using System;
using MediatR;
using TakweneMusic.Application.Common.Interfaces;

namespace TakweneMusic.Application.Dsps.Commands.DeleteDsp;

public record DeleteDspCommand(Guid Id) : ICommand<bool>;
