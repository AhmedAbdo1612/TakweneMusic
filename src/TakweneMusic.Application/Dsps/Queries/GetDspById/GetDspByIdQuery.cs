using System;
using MediatR;
using TakweneMusic.Application.Dsps.Common;

namespace TakweneMusic.Application.Dsps.Queries.GetDspById;

public record GetDspByIdQuery(Guid Id) : IRequest<DspDto>;
