using AutoMapper;
using Core.Dtos.Project;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries
{
    public class GetAllProjectsQuaryHandler : IRequestHandler<GetAllProjectsQuary, IEnumerable<ProjectDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllProjectsQuaryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuary request, CancellationToken cancellationToken)
        {
            var projectList = _mapper.Map<List<Project>, List<ProjectDto>>(await _context.Projects.Include(x => x.Creator).ToListAsync());

            if (projectList == null)
            {
                return null;
            }

            return projectList.AsReadOnly();
        }
    }
}
