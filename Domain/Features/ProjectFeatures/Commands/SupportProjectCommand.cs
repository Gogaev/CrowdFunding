using MediatR;

namespace Core.Dtos.Project
{
    public record SupportProjectCommand(string ProjectId, decimal MoneyAmmount) : IRequest;
}
