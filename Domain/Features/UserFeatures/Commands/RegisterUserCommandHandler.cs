using AutoMapper;
using Core.Dtos;
using Core.Dtos.User;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Domain.Features.UserFeatures.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(request.UserName == null || request.Password == null)
            {
                return new Response { Status = ResponseStatus.BadRequest, Message = "Name or password is null" };
            }

            var userExists = await _userManager.FindByNameAsync(request.UserName);

            if (userExists != null)
            {
                return new Response { Status = ResponseStatus.BadRequest, Message = "User already exists!" };
            }

            var user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user =  _mapper.Map(request, user);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new Response { Status = ResponseStatus.InternalServerError, Message = "User creation failed! Please check user details and try again." };
            }

            return new Response { Status = ResponseStatus.Success, Message = "User created successfully!" };
        }
    }

}
