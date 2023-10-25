using AutoMapper;
using Core.Dtos.Project;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries
{
    public class GetAllPublishedProjectsQueryHandler : IRequestHandler<GetAllPublishedProjectsQuery, IEnumerable<PublishedProjectDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllPublishedProjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PublishedProjectDto>> Handle(GetAllPublishedProjectsQuery request, CancellationToken cancellationToken)
        {
            var projectList = await _context.Projects
                .Include(x => x.Creator)
                .Include(x => x.Supporters)
                .ThenInclude(x => x.User)
                .Where(x => x.Status != Status.Draft)
                .ToListAsync();
            var result = _mapper.Map<List<Project>, List<PublishedProjectDto>>(projectList);

            if (projectList == null)
            {
                throw new AppException("No project exists");
            }

            return result;
        }
    }
}
