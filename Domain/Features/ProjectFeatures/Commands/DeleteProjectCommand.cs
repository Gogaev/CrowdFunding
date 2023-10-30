using Domain.Abstract;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record DeleteProjectCommand(string Id) : IRequest
    {
        public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteProjectCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects
                    .Include(x => x.Tiers)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (project is null)
                {
                    throw new NotFoundException("Project doesn't exist!");
                }

                _context.Projects.Remove(project);
                await _context.SaveChanges();
            }
        }
    }
}
