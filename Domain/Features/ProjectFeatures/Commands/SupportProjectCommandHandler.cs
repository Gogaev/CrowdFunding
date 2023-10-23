using Core.Dtos;
using Core.Dtos.Project;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public class SupportProjectCommandHandler : IRequestHandler<SupportProjectCommand>
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

        public async Task Handle(SupportProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.ProjectId);

            if (project is null)
            {
                throw new KeyNotFoundException("Project doesn't exist!");
            }

            var userId = _userService.GetUserId();

            if(userId is null)
            {
                throw new AppException("Can't find current user Id");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                throw new KeyNotFoundException("Can't find user with such Id");
            }

            project.InvestedMoney += request.MoneyAmmount;
            user.SupportedProjects.Add(project);
            await _context.SaveChanges();
        }
    }
}
