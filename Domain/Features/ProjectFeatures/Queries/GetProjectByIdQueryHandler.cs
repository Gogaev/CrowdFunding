using Domain.Abstract;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project>
    {
        private readonly IApplicationDbContext _context;
        public GetProjectByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Project> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.Include(x => x.Tiers).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (project == null)
            {
                return null;
            }
            return project;
        }
    }
}
