using AutoMapper;
using Core.Dtos.Project;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries
{
    public record GetAllProjectsQuery : IRequest<IEnumerable<ProjectDto>>
    {
        public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public GetAllProjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
            {
                var projectList = await _context.Projects
                    .Include(x => x.Creator)
                    .Include(x => x.Supporters)
                    .ThenInclude(x => x.User)
                    .ToListAsync(cancellationToken: cancellationToken);

                return _mapper.Map<List<Project>, List<ProjectDto>>(projectList);
            }
        }
    }
}
