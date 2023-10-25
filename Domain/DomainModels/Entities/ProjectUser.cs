namespace Domain.DomainModels.Entities
{
    public class ProjectUser
    {
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string? ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
