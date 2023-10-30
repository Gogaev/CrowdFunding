using Domain.Abstract;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.UserFeatures.Commands
{
    public record DeleteUserCommand(string Id) : IRequest
    {
        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IApplicationDbContext _context;

            public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
            {
                _userManager = userManager;
                _context = context;
            }

            public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                if (user is null)
                {
                    throw new NotFoundException("User with such id doesn't exist");
                }

                var userProject = await _context.ProjectUsers
                    .FirstOrDefaultAsync(x => x.UserId == request.Id, cancellationToken: cancellationToken);

                if(userProject is not null)
                {
                    _context.ProjectUsers.Remove(userProject);
                    await _context.SaveChanges();
                }

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    throw new AppException("Can't delete user");
                }
            }
        }
    }
}
