using AutoMapper;
using Core.Dtos.Tier;
using Domain.Abstract;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Queries
{
    public record GetTierByIdQuery(string Id) : IRequest<TierDto?>
    {
        public class GetTierByIdQueryHandler : IRequestHandler<GetTierByIdQuery, TierDto?>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetTierByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<TierDto?> Handle(GetTierByIdQuery request, CancellationToken cancellationToken)
            {
                var tier = await _context.Tiers
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (tier is null)
                {
                    throw new NotFoundException("Tier doesn't exists");
                }

                return _mapper.Map<TierDto>(tier);
            }
        }    }
}
