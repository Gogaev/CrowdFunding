using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.ProjectFeatures.Commands
{
    public record SupportProjectCommand(
        string ProjectId,
        decimal MoneyAmount) : IRequest
    {
        public class SupportProjectCommandHandler : IRequestHandler<SupportProjectCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserService _userService;

            public SupportProjectCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager,
                IUserService userService)
            {
                _context = context;
                _userManager = userManager;
                _userService = userService;
            }

            public async Task Handle(SupportProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects
                    .Include(x => x.Tiers)
                    .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken: cancellationToken);

                if (project is null)
                {
                    throw new NotFoundException("Project doesn't exist!");
                }

                var userId = _userService.GetUserId();

                if (userId is null)
                {
                    throw new AppException("Can't find current user Id");
                }

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new KeyNotFoundException("Can't find user with such Id");
                }

                var isUserSupportingProject =
                    _context.ProjectUsers.Any(up => up.UserId == userId && up.ProjectId == project.Id);

                if (!isUserSupportingProject)
                {
                    var userProject = new ProjectUser
                    {
                        UserId = userId,
                        ProjectId = project.Id,
                    };


                    _context.ProjectUsers.Add(userProject);
                }

                project.InvestedMoney += request.MoneyAmount;
                var tiersToUpdate = project.Tiers.Where(x => x.RequiredMoney <= project.InvestedMoney);

                if (tiersToUpdate is not null)
                {
                    foreach (var t in tiersToUpdate)
                    {
                        t.IsReached = true;
                    }

                    _context.Tiers.UpdateRange(tiersToUpdate.ToList());
                }

                if (project.InvestedMoney >= project.RequiredMoney)
                {
                    project.Status = Status.Finished;
                }

                await _context.SaveChanges();
            }
        }
    }
}