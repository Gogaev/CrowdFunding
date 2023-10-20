using MediatR;

namespace Core.Dtos.Project
{
    public record SupportProjectCommand(int ProjectId, decimal MoneyAmmount) : IRequest<Response>;
}
