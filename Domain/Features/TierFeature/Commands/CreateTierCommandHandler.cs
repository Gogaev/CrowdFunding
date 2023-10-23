using AutoMapper;
using Core.Dtos;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using Domain.DomainModels.Exceptions;
using MediatR;

namespace Domain.Features.TierFeature.Commands
{
    public class CreateTierCommandHandler : IRequestHandler<CreateTierCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateTierCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Handle(CreateTierCommand request, CancellationToken cancellationToken)
        {
            var tier = _mapper.Map<Tier>(request);
            tier.Id = Guid.NewGuid().ToString();

            var result = _context.Tiers.Add(tier);
            
            if(result is null)
            {
                throw new AppException("Can't create tier");
            }

            await _context.SaveChanges();
        }
    }
}
