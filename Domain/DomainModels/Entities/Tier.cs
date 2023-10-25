namespace Domain.DomainModels.Entities
{
    public class Tier
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? TierName { get; set; }
        public decimal RequiredMoney { get; set; }
        public string? Benefit { get; set; }
        public bool IsReached { get; set; } = false;
        public string? ProjectId { get; set; }
        public Project Project { get; set; } = new Project();
    }
}
