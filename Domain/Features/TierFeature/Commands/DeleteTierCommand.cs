using Domain.Abstract;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Commands
{
    public record DeleteTierCommand(string Id) : IRequest
    {
        public class DeleteTierCommandHandler : IRequestHandler<DeleteTierCommand>
        {
            private readonly IApplicationDbContext _context;
            public DeleteTierCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task Handle(DeleteTierCommand request, CancellationToken cancellationToken)
            {
                var tier = await _context.Tiers
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (tier is null)
                {
                    throw new NotFoundException("Tier doesn't exists");
                }

                tier.isDeleted = true;
                
                await _context.SaveChanges();
            }
        }
    }
}
