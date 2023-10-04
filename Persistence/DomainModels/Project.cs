
namespace Persistence.DomainModels
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Status Status { get; set; }
        public DateTime StartingDay { get; set; }
        public DateTime LastDay { get; set; }
        public decimal RequiredMoney { get; set; }
        public decimal InvestedMoney { get; set; }
        public ICollection<Tier> Tiers { get; set; } = new List<Tier>();

    }
}
