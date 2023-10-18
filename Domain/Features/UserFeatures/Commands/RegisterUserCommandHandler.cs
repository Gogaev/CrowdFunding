using AutoMapper;
using Core.Dtos;
using Core.Dtos.User;
using Domain.DomainModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
                return new Response { Status = "Error", Message = "Name or password is null" };
            }

            var userExists = await _userManager.FindByNameAsync(request.UserName);

            if (userExists != null)
            {
                return new Response { Status = "Error", Message = "User already exists!" };
            }
            var user = _mapper.Map<ApplicationUser>(request);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." };
            }

            return new Response { Status = "Success", Message = "User created successfully!" };
        }
    }

}
