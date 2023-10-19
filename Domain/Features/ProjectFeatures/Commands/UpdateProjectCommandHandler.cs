using Core.Dtos;
using Domain.Abstract;
using Domain.DomainModels.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Response>
    {
        private readonly IApplicationDbContext _context;
        public UpdateProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Response> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (project == null)
            {
                return new Response { Status = ResponseStatus.NotFound, Message = "Project doesn't exist!" };
            }
            else
            {
                project.Title = request.Title;
                project.Description = request.Description;
                project.ImageUrl = request.ImageUrl;
                project.Status = request.Status;
                project.StartingDay = request.StartingDay;
                project.LastDay = request.LastDay;
                project.RequiredMoney = request.RequiredMoney;
                project.InvestedMoney = request.InvestedMoney;
                await _context.SaveChanges();
                return new Response { Status = ResponseStatus.Success, Message = "Project was updated successfully" };
            }
        }
    }
}
