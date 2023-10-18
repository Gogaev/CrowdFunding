using Core.Dtos.Tier;
using MediatR;

namespace Domain.Features.TierFeature.Queries
{
    public record GetTierByIdQuary(int Id) : IRequest<TierDto?>;
}
