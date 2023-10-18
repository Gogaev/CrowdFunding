using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.User
{
    public class RegisterUserCommand
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
    }
}
