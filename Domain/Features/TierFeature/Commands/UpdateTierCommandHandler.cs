using AutoMapper;
using Domain.Abstract;
using Domain.DomainModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Commands;

public class UpdateTierCommandHandler : IRequestHandler<UpdateTierCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateTierCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateTierCommand request, CancellationToken cancellationToken)
    {
        var tier = await _context.Tiers.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (tier == null)
        {
            return default;
        }
        else
        {
            _mapper.Map<UpdateTierCommand, Tier>(request, tier);
            await _context.SaveChanges();
            return tier.Id;
        }
    }
}
