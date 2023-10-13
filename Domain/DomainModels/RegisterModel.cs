using System.ComponentModel.DataAnnotations;

namespace Domain.DomainModels
{
    public class RegisterModel
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
