using AutoMapper;
using Core.Dtos.Tier;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Queries
{
    public class GetAllTiersQueryHandler : IRequestHandler<GetAllTiersQuary, IEnumerable<TierDto>?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllTiersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TierDto>?> Handle(GetAllTiersQuary request, CancellationToken cancellationToken)
        {
            var tierList = _mapper.Map<List<Tier>, List<TierDto>>(await _context.Tiers.ToListAsync());
            if (tierList == null)
            {
                return null;
            }
            return tierList.AsReadOnly();
        }
    }
}
