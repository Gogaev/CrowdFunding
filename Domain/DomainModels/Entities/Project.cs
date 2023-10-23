using Domain.DomainModels.Enums;

namespace Domain.DomainModels.Entities
{
    public class Project
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Status Status { get; set; }
        public DateTime StartingDay { get; set; }
        public DateTime LastDay { get; set; }
        public decimal RequiredMoney { get; set; }
        public decimal InvestedMoney { get; set; }
        public List<Tier> Tiers { get; set; } = new List<Tier>();
        public string? CreatorId { get; set; }
        public ApplicationUser? Creator { get; set; }
        public List<ApplicationUser> Supporters { get; set; } = new List<ApplicationUser>();

    }
}
