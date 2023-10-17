using AutoMapper;
using Domain.Abstract;
using Domain.DomainModels;
using MediatR;

namespace Domain.Features.TierFeature.Commands
{
    public class CreateTierCommandHandler : IRequestHandler<CreateTierCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateTierCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateTierCommand request, CancellationToken cancellationToken)
        {
            var tier = _mapper.Map<Tier>(request);
            _context.Tiers.Add(tier);
            await _context.SaveChanges();
            return tier.Id;
        }
    }
}
