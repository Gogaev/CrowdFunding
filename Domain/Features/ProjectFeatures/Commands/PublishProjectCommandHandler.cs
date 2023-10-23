using Domain.Abstract;
using Domain.DomainModels.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public class PublishProjectCommandHandler : IRequestHandler<PublishProjectCommand>
    {
        private readonly IApplicationDbContext _context;

        public PublishProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(PublishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);
            
            if (project == null)
            {
                throw new NullReferenceException("Project doesn't exist");
            }

            project.Status = Status.Published;
            project.StartingDay = DateTime.UtcNow;

            await _context.SaveChanges();
        }
    }
}
