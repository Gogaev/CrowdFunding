﻿using AutoMapper;
using Core.Dtos.Project;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries
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
                .ToListAsync();

            var result = _mapper.Map<List<Project>, List<ProjectDto>>(projectList);

            if (projectList == null)
            {
                throw new AppException("No project exists");
            }

            return result;
        }
    }
}
