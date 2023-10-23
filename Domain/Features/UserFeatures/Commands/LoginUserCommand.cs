using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.User
{
    public class LoginUserCommand : IRequest<Response>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
