using AutoMapper;
using Core.Dtos.Tier;
using Domain.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Queries
{
    public class GetTierByIdQueryHandler : IRequestHandler<GetTierByIdQuary, TierDto?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTierByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TierDto?> Handle(GetTierByIdQuary request, CancellationToken cancellationToken)
        {
            var tier = await _context.Tiers.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (tier == null)
            {
                return null;
            }
            return _mapper.Map<TierDto>(tier);
        }
    }
}
