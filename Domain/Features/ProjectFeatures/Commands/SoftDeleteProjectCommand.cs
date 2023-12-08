using Domain.Abstract;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record SoftDeleteProjectCommand(string id) : IRequest
    {
        public class SoftDeleteProjectCommandHandler : IRequestHandler<SoftDeleteProjectCommand>
        {
            private readonly IApplicationDbContext _context;

            public SoftDeleteProjectCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(SoftDeleteProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects
                    .Include(x => x.Tiers)
                    .FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken: cancellationToken);

                if (project is null)
                {
                    throw new NotFoundException("Project doesn't exist!");
                }

                //_context.Projects.Remove(project);
                project.IsDeleted = true;
                await _context.SaveChanges();
            }
        }
    }
}
