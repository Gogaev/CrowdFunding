
namespace Core.Dtos.User
{
    public class UserDto
    {
        public string Name { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email{ get; set; } = "";
        public string Description{ get; set; } = "";
        public List<string> CreatedProjects { get; set; } = new List<string>();
    }
}
