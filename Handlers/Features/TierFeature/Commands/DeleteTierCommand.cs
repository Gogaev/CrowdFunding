using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Domain.Features.TierFeature.Commands
{
    public class DeleteTierCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteTierCommandHandler : IRequestHandler<DeleteTierCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteTierCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteTierCommand request, CancellationToken cancellationToken)
            {
                var tier = await _context.Tiers.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (tier == null)
                {
                    return default;
                }
                _context.Tiers.Remove(tier);
                await _context.SaveChanges();
                return tier.Id;
            }
        }
    }
}
