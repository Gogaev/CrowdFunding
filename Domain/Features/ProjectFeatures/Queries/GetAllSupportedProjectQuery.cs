using AutoMapper;
using Core.Dtos.Project;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries;

public record GetAllSupportedProjectQuery : IRequest<IEnumerable<ProjectDto>>
{
    public class GetAllSupportedProjectQueryHandler : IRequestHandler<GetAllSupportedProjectQuery, IEnumerable<ProjectDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetAllSupportedProjectQueryHandler(IApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        
        public async Task<IEnumerable<ProjectDto>> Handle(GetAllSupportedProjectQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _userService.GetUserId();
            var projectList = await _context.Projects
                .Where(p => p.Supporters.Any(u => u.UserId == currentUserId))
                //.Where(x => !x.IsDeleted)
                .ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<Project>, List<ProjectDto>>(projectList);
        }
    }
}