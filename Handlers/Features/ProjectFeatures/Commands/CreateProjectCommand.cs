using MediatR;
using Persistence;
using Persistence.Context;
using Persistence.DomainModels;

namespace Handlers.Features.ProjectFeatures.Commands
{
    public class CreateProjectCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartingDay { get; set; }
        public DateTime LastDay { get; set; }
        public decimal RequiredMoney { get; set; }

        public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateProjectCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
            {
                var project = new Project();
                project.Title = request.Title;
                project.Description = request.Description;
                project.ImageUrl = request.ImageUrl;
                project.Status = Status.Draft;
                project.StartingDay = request.StartingDay;
                project.LastDay = request.LastDay;
                project.RequiredMoney = request.RequiredMoney;
                project.InvestedMoney = 0;
                _context.Projects.Add(project);
                await _context.SaveChanges();
                return project.Id;
            }
        }
    }
}
