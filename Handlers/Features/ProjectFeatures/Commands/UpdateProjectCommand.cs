using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Context;

namespace Handlers.Features.ProjectFeatures.Commands
{
    public class UpdateProjectCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Status Status { get; set; }
        public DateTime StartingDay { get; set; }
        public DateTime LastDay { get; set; }
        public decimal RequiredMoney { get; set; }
        public decimal InvestedMoney { get; set; }

        public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateProjectCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (project == null)
                {
                    return default;
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
                    return project.Id;
                }
            }
        }
    }
}
