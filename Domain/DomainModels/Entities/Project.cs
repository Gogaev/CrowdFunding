using Domain.DomainModels.Enums;

namespace Domain.DomainModels.Entities
{
    public class Project
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = "Draft project";
        //public string Subject { get; set;}
        //public string ShortDescription { get; set;}
        //public string[] Images { get; set;}
        public string Description { get; set; } = "Draft Description";
        public string ImageUrl { get; set; } = "";
        public Status Status { get; set; }
        public DateTime StartingDay { get; set; }
        public DateTime? LastDay { get; set; }
        public decimal RequiredMoney { get; set; }
        public decimal InvestedMoney { get; set; }
        public List<Tier> Tiers { get; set; } = new List<Tier>();
        public string? CreatorId { get; set; }
        public ApplicationUser? Creator { get; set; }
        public List<ProjectUser> Supporters { get; set; } = new List<ProjectUser>();

    }
}
