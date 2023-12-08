using AutoMapper;
using Domain.Abstract;
using Domain.DomainModels.Enums;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record UpdateProjectCommand(
        string Id,
        string Title,
        string Description,
        string ImageUrl,
        DateTime LastDay,
        decimal RequiredMoney) : IRequest
    {
        public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public UpdateProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (project is null)
                {
                    throw new NotFoundException("Project doesn't exist!");
                }

                _mapper.Map(request, project);
                await _context.SaveChanges();
            }
        }
    }
}
