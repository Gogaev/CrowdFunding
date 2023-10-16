namespace Core.Dtos.Tier
{
    public class CreateTierDto
    {
        public string TierName { get; set; } = "";
        public decimal RequiredMoney { get; set; }
        public string Benefit { get; set; } = "";
        public int ProjectId { get; set; }
    }
}
