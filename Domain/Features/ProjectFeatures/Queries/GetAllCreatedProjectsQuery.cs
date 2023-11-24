using AutoMapper;
using Core.Dtos.Project;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries;

public record GetAllCreatedProjectsQuery(Status status) : IRequest<IEnumerable<ProjectWithTiersDto>>
{
    public class GetAllCreatedProjectsQueryHandler : IRequestHandler<GetAllCreatedProjectsQuery, IEnumerable<ProjectWithTiersDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAllCreatedProjectsQueryHandler(IApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        
        public async Task<IEnumerable<ProjectWithTiersDto>> Handle(GetAllCreatedProjectsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Project> query = _context.Projects;
            if (request.status != Status.All)
            {
                 query = _context.Projects
                    .Where(x => x.Status == request.status);
            }
            var currentUserId = _userService.GetUserId();
            query = query.Where(x => x.CreatorId == currentUserId);
            var projectList = await query
                .OrderBy(x => x.Status == Status.Expired)
                .ToListAsync(cancellationToken: cancellationToken);
            return _mapper.Map<List<Project>, List<ProjectWithTiersDto>>(projectList);
        }
    }
}