using Domain.DomainModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Domain.Features.ProjectFeatures.Queries
{
    public class GetProjectByIdQuery : IRequest<Project>
    {
        public int Id { get; set; }
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
}
