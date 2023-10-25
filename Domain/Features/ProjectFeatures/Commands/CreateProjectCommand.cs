using MediatR;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record CreateProjectCommand(
        string Title,
        string Description,
        string ImageUrl,
        DateTime LastDay,
        decimal RequiredMoney
        ) : IRequest;
}
