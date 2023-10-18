using Domain.Abstract;
using Domain.DomainModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries
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
            var projectList = await _context.Projects.Include(x => x.Tiers).Include(x => x.Creator).ToListAsync();
            if (projectList == null)
            {
                return null;
            }
            return projectList.AsReadOnly();
        }
    }
}
