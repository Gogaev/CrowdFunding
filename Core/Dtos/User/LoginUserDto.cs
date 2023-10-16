using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.User
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
