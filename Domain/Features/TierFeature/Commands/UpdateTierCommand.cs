using Core.Dtos;
using MediatR;

namespace Domain.Features.TierFeature.Commands
{
    public record UpdateTierCommand(int Id, string? TierName, decimal RequiredMoney, string Benefit, bool IsReached, int ProjectId) : IRequest<Response>;
}
