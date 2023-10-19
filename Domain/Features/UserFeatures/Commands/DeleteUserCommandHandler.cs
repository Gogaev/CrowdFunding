using Core.Dtos;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.UserFeatures.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user == null)
            {
                return new Response { Status = ResponseStatus.NotFound, Message = "User doesn't exist!" };
            }

            var result = await _userManager.DeleteAsync(user);

            if(!result.Succeeded)
            {
                return new Response { Status = ResponseStatus.InternalServerError, Message = "Can't delete user" };
            }

            return new Response { Status = ResponseStatus.Success, Message = "User deleted successfully" };
        }
    }
}
