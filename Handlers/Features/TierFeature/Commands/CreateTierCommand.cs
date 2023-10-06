using MediatR;
using Persistence.Context;
using Persistence.DomainModels;

namespace Domain.Features.TierFeature.Commands
{
    public class CreateTierCommand : IRequest<int>
    {
        public string TierName { get; set; }
        public decimal RequiredMoney { get; set; }
        public string Benefit { get; set; }
        public bool IsReached { get; set; }
        public int ProjectId { get; set; }
        public class CreateTierCommandHandler : IRequestHandler<CreateTierCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateTierCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateTierCommand request, CancellationToken cancellationToken)
            {
                var tier = new Tier();
                tier.TierName = request.TierName;
                tier.RequiredMoney = request.RequiredMoney;
                tier.Benefit = request.Benefit;
                tier.IsReached = request.IsReached;
                tier.ProjectId = request.ProjectId;
                _context.Tiers.Add(tier);
                await _context.SaveChanges();
                return tier.Id;
            }
        }
    }
}
