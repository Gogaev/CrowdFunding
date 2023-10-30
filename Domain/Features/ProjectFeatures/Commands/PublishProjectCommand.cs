using Domain.Abstract;
using Domain.DomainModels.Enums;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record PublishProjectCommand(string Id) : IRequest
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
                var project = await _context.Projects
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (project is null)
                {
                    throw new NotFoundException("Project doesn't exist!");
                }

                project.Status = Status.Published;
                project.StartingDay = DateTime.UtcNow;
                if(project.LastDay == DateTime.MinValue)
                {
                    project.LastDay = DateTime.UtcNow.AddMonths(6);
                }
                await _context.SaveChanges();
            }
        }    }
}
