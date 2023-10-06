using Domain.DomainModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Domain.Features.TierFeature.Queries
{
    public class GetAllTiersQuary : IRequest<IEnumerable<Tier>>
    {
        public class GetAllTiersQueryHandler : IRequestHandler<GetAllTiersQuary, IEnumerable<Tier>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllTiersQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Tier>> Handle(GetAllTiersQuary request, CancellationToken cancellationToken)
            {
                var tierList = await _context.Tiers.ToListAsync();
                if (tierList == null)
                {
                    return null;
                }
                return tierList.AsReadOnly();
            }
        }
    }
}
