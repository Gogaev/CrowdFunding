using Microsoft.AspNetCore.Identity;

namespace Domain.DomainModels.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public List<Project> CreatedProjects { get; set; } = new List<Project>();
        public List<ProjectUser> SupportedProjects { get; set; } = new List<ProjectUser>();
    }
}
