namespace Core.Dtos.Tier
{
    public class TierDto
    {
        public string TierName { get; set; } = "";
        public decimal RequiredMoney { get; set; }
        public bool IsReached { get; set; }
        public string Benefit { get; set; } = "";
    }
}