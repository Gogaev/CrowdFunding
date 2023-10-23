using Core.Dtos;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.UserFeatures.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user is null)
            {
                throw new KeyNotFoundException("User with such id doesn't exist");
            }

            var result = await _userManager.DeleteAsync(user);

            if(!result.Succeeded)
            {
                throw new AppException("Can't delete user");
            }
        }
    }
}
