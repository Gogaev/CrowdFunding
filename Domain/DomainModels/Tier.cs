
namespace Domain.DomainModels
{
    public class Tier
    {
        public int Id { get; set; }
        public string TierName { get; set; }
        public decimal RequiredMoney { get; set; }
        public string Benefit { get; set; }
        public bool IsReached { get; set; } = false;
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
