using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Handlers.Features.ProjectFeatures.Commands
{
    public class DeleteProjectCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteProjectCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects.Include(x => x.Tiers).FirstOrDefaultAsync(x => x.Id == request.Id);
                if (project == null)
                {
                    return default;
                }
                _context.Projects.Remove(project);
                await _context.SaveChanges();
                return project.Id;
            }
        }
    }
}
