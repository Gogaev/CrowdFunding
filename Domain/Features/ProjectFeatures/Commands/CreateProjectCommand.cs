using Core.Dtos;
using MediatR;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record CreateProjectCommand(
        string Title,
        string Description,
        string ImageUrl,
        DateTime StartingDay,
        DateTime LastDay,
        decimal RequiredMoney
        ) : IRequest;
}
