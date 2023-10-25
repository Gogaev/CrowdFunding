using MediatR;

namespace Domain.Features.TierFeature.Commands
{
    public record UpdateTierCommand(string Id, string? TierName, decimal RequiredMoney, string Benefit, bool IsReached, string ProjectId) : IRequest;
}
