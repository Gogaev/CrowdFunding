using MediatR;

namespace Domain.Features.TierFeature.Commands
{
    public class CreateTierCommand : IRequest<int>
    {
        public string TierName { get; set; } = "";
        public decimal RequiredMoney { get; set; }
        public string Benefit { get; set; } = "";
        public int ProjectId { get; set; }
        
    }
}
