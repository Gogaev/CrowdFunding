using AutoMapper;
using Core.Dtos.Project;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries;

public record GetAllCreatedProjects : IRequest<IEnumerable<ProjectDto>>
{
    public class GetAllCreatedProjectsHandler : IRequestHandler<GetAllCreatedProjects, IEnumerable<ProjectDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAllCreatedProjectsHandler(IApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        
        public async Task<IEnumerable<ProjectDto>> Handle(GetAllCreatedProjects request, CancellationToken cancellationToken)
        {
            var currentUserId = _userService.GetUserId();
            var projectList = await _context.Projects
                .Where(x => x.CreatorId == currentUserId)
                .ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<Project>, List<ProjectDto>>(projectList);
        }
    }
}