using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Core.Dtos.User;
using Domain.Abstract;
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
        string? Role) : IRequest<LoginDto>
    {
        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, LoginDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager, IConfiguration config, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _config = config;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim("UserId", user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var token = _jwtTokenService.GetToken(authClaims);

            var tokenHandler = new JwtSecurityTokenHandler();
            await _userManager.SetAuthenticationTokenAsync(user, TokenOptions.DefaultProvider, "LoginToken", tokenHandler.WriteToken(token));

            return new LoginDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expired = token.ValidTo
            };
            
        }
    }
    }
}
