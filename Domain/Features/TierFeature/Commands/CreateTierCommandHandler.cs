using AutoMapper;
using Core.Dtos;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using MediatR;

namespace Domain.Features.TierFeature.Commands
{
    public class CreateTierCommandHandler : IRequestHandler<CreateTierCommand, Response>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateTierCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response> Handle(CreateTierCommand request, CancellationToken cancellationToken)
        {
            var tier = _mapper.Map<Tier>(request);
            _context.Tiers.Add(tier);
            await _context.SaveChanges();
            return new Response { Status = ResponseStatus.Success, Message = "Tier was created successfully" };
        }
    }
}
