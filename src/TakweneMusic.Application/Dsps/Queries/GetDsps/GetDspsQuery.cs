using System.Collections.Generic;
using MediatR;
using TakweneMusic.Application.Dsps.Common;

namespace TakweneMusic.Application.Dsps.Queries.GetDsps;

public record GetDspsQuery : IRequest<List<DspDto>>;
