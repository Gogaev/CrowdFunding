using Core.Dtos;
using Domain.Abstract;
using Domain.DomainModels.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Response>
    {
        private readonly IApplicationDbContext _context;
        public DeleteProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Response> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.Include(x => x.Tiers).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (project == null)
            {
                return new Response { Status = ResponseStatus.NotFound, Message = "Project doesn't exist!" };
            }
            _context.Projects.Remove(project);
            await _context.SaveChanges();
            return new Response { Status = ResponseStatus.Success, Message = "Project was deleted successfully" };
        }
    }
}
