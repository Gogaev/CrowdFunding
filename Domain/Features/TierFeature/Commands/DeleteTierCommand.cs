using MediatR;

namespace Domain.Features.TierFeature.Commands
{
    public record DeleteTierCommand(int Id) : IRequest<int>;
}
