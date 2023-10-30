using AutoMapper;
using Core.Dtos.Project;
using Domain.Abstract;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries
{
    public record GetProjectByIdQuery(string Id) : IRequest<ProjectWithTiersDto>
    {
        public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectWithTiersDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public GetProjectByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<ProjectWithTiersDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects
                    .Include(x => x.Creator)
                    .Include(x => x.Tiers)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                var result = _mapper.Map<ProjectWithTiersDto>(project);

                if (project == null)
                {
                    throw new NotFoundException("Project with such id doesn't exist");
                }

                return result;
            }
        }
    }
}
