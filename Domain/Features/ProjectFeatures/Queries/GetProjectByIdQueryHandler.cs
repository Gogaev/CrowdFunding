using AutoMapper;
using Core.Dtos.Project;
using Domain.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Queries
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
            var project = await _context.Projects.Include(x => x.Creator).Include(x => x.Tiers).FirstOrDefaultAsync(x => x.Id == request.Id);

            var result = _mapper.Map<ProjectWithTiersDto>(project);

            if (project == null)
            {
                throw new KeyNotFoundException("Project with such id doesn't exist");
            }

            return result;
        }
    }
}
