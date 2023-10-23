using AutoMapper;
using Core.Dtos;
using Domain.Abstract;
using Domain.DomainModels.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Commands;

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
        var tier = await _context.Tiers.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (tier is null)
        {
            throw new KeyNotFoundException("Tier doesn't exists");
        }

        else
        {
            _mapper.Map(request, tier);
            await _context.SaveChanges();
        }
    }
}
