using Core.Dtos;
using Core.Dtos.Project;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public class SupportProjectCommandHandler : IRequestHandler<SupportProjectCommand, Response>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public SupportProjectCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<Response> Handle(SupportProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.ProjectId);

            if (project == null)
            {
                return new Response { Status = ResponseStatus.NotFound, Message = "Project doesn't exist" };
            }



            var userId = _userService.GetUserId();

            if(userId == null)
            {
                return new Response { Status = ResponseStatus.InternalServerError, Message = "Can't find current user" };
            }

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return new Response { Status = ResponseStatus.InternalServerError, Message = "Can't find current user" };
            }

            project.InvestedMoney += request.MoneyAmmount;
            user.SupportedProjects.Add(project);
            await _context.SaveChanges();

            return new Response { Status = ResponseStatus.Success, Message = "Project was supported" };
        }
    }
}
