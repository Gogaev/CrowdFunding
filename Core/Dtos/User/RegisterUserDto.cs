using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.User
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "User name is required!")]
        public string? UserName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
