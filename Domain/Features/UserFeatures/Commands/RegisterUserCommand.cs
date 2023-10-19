using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.User
{
    public class RegisterUserCommand : IRequest<Response>
    {
        [Required(ErrorMessage = "User name is required!")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Name and Surname is Required")]
        public string? FullName { get; set; }
        public string Description { get; set; } = "";
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? AdminKey { get; set; }
        public string? Role { get; set; }
    }
}
