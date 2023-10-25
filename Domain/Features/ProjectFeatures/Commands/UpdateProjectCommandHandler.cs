using AutoMapper;
using Domain.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (project is null)
            {
                throw new KeyNotFoundException("Project doesn't exist!");
            }
            project = _mapper.Map(request, project);

            await _context.SaveChanges();
        }
    }
}
