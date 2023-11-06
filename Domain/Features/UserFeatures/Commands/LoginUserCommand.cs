using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Dtos.User;
using Domain.Abstract;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Domain.Features.UserFeatures.Commands
{
    public record LoginUserCommand(string? Username, string? Password) : IRequest<LoginDto>
    {
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if(request.Username is null || request.Password is null)
            {
                throw new NullReferenceException("Username or password is null");
            }

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user?.UserName == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Name or password is wrong");
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
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

        }
    }
    }
}
