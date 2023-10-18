using Core.Dtos;
using MediatR;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record UpdateProjectCommand(
        int Id,
        string Title,
        string Description,
        string ImageUrl,
        Status Status,
        DateTime StartingDay,
        DateTime LastDay,
        decimal RequiredMoney,
        decimal InvestedMoney
        ) : IRequest<Response>;
}
