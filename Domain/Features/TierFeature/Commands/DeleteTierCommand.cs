﻿using Core.Dtos;
using MediatR;

namespace Domain.Features.TierFeature.Commands
{
    public record DeleteTierCommand(int Id) : IRequest<Response>;
}
