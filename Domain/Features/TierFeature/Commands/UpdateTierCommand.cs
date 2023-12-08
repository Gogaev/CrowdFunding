using AutoMapper;
using Domain.Abstract;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Commands
{
    public record UpdateTierCommand(
        string Id,
        decimal RequiredMoney,
        string Benefit) : IRequest
    {
        public class UpdateTierCommandHandler : IRequestHandler<UpdateTierCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateTierCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Handle(UpdateTierCommand request, CancellationToken cancellationToken)
            {
                var tier = await _context.Tiers
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (tier is null)
                {
                    throw new NotFoundException("Tier doesn't exists");
                }

                _mapper.Map(request, tier);
                await _context.SaveChanges();
            }
        }
    }
}