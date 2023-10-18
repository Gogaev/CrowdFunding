using Core.Dtos.User;
using MediatR;

namespace Domain.Features.UserFeatures.Queries
{
    public record GetUserByIdQuery(string Id) : IRequest<UserDto>;
}
