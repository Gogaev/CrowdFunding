using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.User
{
    public class RegisterUserCommand : IRequest
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string Description { get; set; } = "";
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? AdminKey { get; set; }
        public string? Role { get; set; }
    }
}
