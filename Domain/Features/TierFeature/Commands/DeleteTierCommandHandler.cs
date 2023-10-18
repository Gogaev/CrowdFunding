using Core.Dtos;
using Domain.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Commands
{
    public class DeleteTierCommandHandler : IRequestHandler<DeleteTierCommand, Response>
    {
        private readonly IApplicationDbContext _context;
        public DeleteTierCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Response> Handle(DeleteTierCommand request, CancellationToken cancellationToken)
        {
            var tier = await _context.Tiers.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (tier == null)
            {
                return default;
            }
            _context.Tiers.Remove(tier);
            await _context.SaveChanges();
            return new Response { Status = "Success", Message = "Tier was deleted successfully" };
        }
    }
}
