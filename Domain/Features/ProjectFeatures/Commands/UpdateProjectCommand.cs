using Domain.DomainModels.Enums;
using MediatR;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record UpdateProjectCommand(
        string Id,
        string Title,
        string Description,
        string ImageUrl,
        Status Status,
        DateTime LastDay,
        decimal RequiredMoney,
        decimal InvestedMoney
        ) : IRequest;
}
