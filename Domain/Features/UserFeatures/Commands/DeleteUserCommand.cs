using MediatR;

namespace Domain.Features.UserFeatures.Commands
{
    public record DeleteUserCommand(string Id) : IRequest;
}
