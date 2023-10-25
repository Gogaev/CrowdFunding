using Domain.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Commands
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
            var tier = await _context.Tiers.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (tier is null)
            {
                throw new KeyNotFoundException("Tier doesn't exists");
            }

            _context.Tiers.Remove(tier);
            await _context.SaveChanges();
        }
    }
}
