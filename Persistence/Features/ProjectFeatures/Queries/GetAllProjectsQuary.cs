using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.DomainModels;

namespace Persistence.Features.ProjectFeatures.Queries
{
    public class GetAllProjectsQuary : IRequest<IEnumerable<Project>>
    {
        public class GetAllProjectsQuaryHandler : IRequestHandler<GetAllProjectsQuary, IEnumerable<Project>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllProjectsQuaryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Project>> Handle(GetAllProjectsQuary request, CancellationToken cancellationToken)
            {
                var projectList = await _context.Projects.Include(x => x.Tiers).ToListAsync();
                if (projectList == null)
                {
                    return null;
                }
                return projectList.AsReadOnly();
            }
        }
    }
}
