using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Domain.Features.TierFeature.Commands
{
    public class UpdateTierCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string TierName { get; set; }
        public decimal RequiredMoney { get; set; }
        public string Benefit { get; set; }
        public bool IsReached { get; set; }
        public int ProjectId { get; set; }
        public class UpdateTierCommandHandler : IRequestHandler<UpdateTierCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateTierCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateTierCommand request, CancellationToken cancellationToken)
            {
                var tier = await _context.Tiers.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (tier == null)
                {
                    return default;
                }
                else
                {
                    tier.TierName = request.TierName;
                    tier.RequiredMoney = request.RequiredMoney;
                    tier.Benefit = request.Benefit;
                    tier.IsReached = request.IsReached;
                    tier.ProjectId = request.ProjectId;
                    await _context.SaveChanges();
                    return tier.Id;
                }
            }
        }
    }
}
