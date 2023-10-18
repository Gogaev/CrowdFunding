using Core.Dtos.Tier;
using MediatR;

namespace Domain.Features.TierFeature.Queries
{
    public record GetAllTiersQuary() : IRequest<IEnumerable<TierDto>?>;
}
