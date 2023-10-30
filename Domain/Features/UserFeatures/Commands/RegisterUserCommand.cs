using AutoMapper;
using Domain.DomainModels.Constants;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Domain.Features.UserFeatures.Commands
{
    public record RegisterUserCommand(
        string? UserName,
        string? FullName,
        string Description,
        string? EmailAddress,
        string? Password,
        string? AdminKey,
        string? Role) : IRequest
    {
        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(request.UserName == null || request.Password == null)
            {
                throw new AppException("Name or password is null");
            }

            var userExists = await _userManager.FindByNameAsync(request.UserName);

            if (userExists != null)
            {
                throw new AppException("User already exists!");
            }

            var user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user =  _mapper.Map(request, user);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User creation failed! Please check user details and try again.");
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if(request.Role is not null && request.Role.ToLower() == UserRoles.Admin)
            {
                if(request.AdminKey is null || request.AdminKey != _config["KeyForAdminRegister"])
                {
                    throw new AppException("Wrong admin key");
                }

                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                }
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
        }
    }
    }
}
