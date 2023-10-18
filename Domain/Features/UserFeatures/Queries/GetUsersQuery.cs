using Core.Dtos.User;
using MediatR;

namespace Domain.Features.UserFeatures.Queries
{
    public record GetUsersQuery() : IRequest<IEnumerable<UserDto>>;
}
