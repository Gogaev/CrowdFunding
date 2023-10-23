using Core.Dtos.Tier;
using MediatR;

namespace Domain.Features.TierFeature.Queries
{
    public record GetTierByIdQuary(string Id) : IRequest<TierDto?>;
}
