using Core.Dtos;
using MediatR;

namespace Domain.Features.TierFeature.Commands
{
    public record CreateTierCommand(string? TierName, decimal RequiredMoney, string? Benefit, int ProjectId) : IRequest<Response>;
}
