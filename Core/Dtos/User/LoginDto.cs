
namespace Core.Dtos.User
{
    public class LoginDto
    {
        public string? Token { get; set; }
        public DateTime? Expired { get; set; }
    }
}
