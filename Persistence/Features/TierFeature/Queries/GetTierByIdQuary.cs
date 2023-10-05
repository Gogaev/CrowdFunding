using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.DomainModels;

namespace Persistence.Features.TierFeature.Queries
{
    public class GetTierByIdQuary : IRequest<Tier>
    {
        public int Id { get; set; }
        public class GetTierByIdQueryHandler : IRequestHandler<GetTierByIdQuary, Tier>
        {
            private readonly IApplicationDbContext _context;
            public GetTierByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Tier> Handle(GetTierByIdQuary request, CancellationToken cancellationToken)
            {
                var tier = await _context.Tiers.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (tier == null)
                {
                    return null;
                }
                return tier;
            }
        }
    }
}
