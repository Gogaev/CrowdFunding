using Domain.DomainModels.Entities;
using MediatR;

namespace Core.Dtos.User
{
    public class LoginUserCommand : IRequest<LoginDto>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
