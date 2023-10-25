using MediatR;

namespace Domain.Features.TierFeature.Commands
{
    public record DeleteTierCommand(string Id) : IRequest;
}
