using Core.Dtos;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using MediatR;

namespace Domain.Features.ProjectFeatures.Commands
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Response>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;
        public CreateProjectCommandHandler(IApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Response> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
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
            project.CreatorId = _userService.GetUserId();
            _context.Projects.Add(project);
            await _context.SaveChanges();
            return new Response { Status = ResponseStatus.Success, Message = "Project was created successfully" };
        }
    }
}
