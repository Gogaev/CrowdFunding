using AutoMapper;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.TierFeature.Commands
{
    public record CreateTierCommand(
        string? TierName,
        decimal RequiredMoney,
        string? Benefit,
        string ProjectId) : IRequest
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

                var project = await _context.Projects
                    .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken: cancellationToken);

                if(project is null)
                {
                    throw new KeyNotFoundException("Project with this id doesn't exist");
                }

                project.Tiers.Add(tier);

                await _context.SaveChanges();
            }
        }
    }
}
