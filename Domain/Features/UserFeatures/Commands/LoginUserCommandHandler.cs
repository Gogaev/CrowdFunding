using Core.Dtos.User;
using Domain.DomainModels.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Features.UserFeatures.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if(request.Username is null || request.Password is null)
            {
                throw new NullReferenceException("Username or password is null");
            }

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user != null && user.UserName != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim("UserId", user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                var tokenHandler = new JwtSecurityTokenHandler();
                await _userManager.SetAuthenticationTokenAsync(user, TokenOptions.DefaultProvider, "LoginToken", tokenHandler.WriteToken(token));

                return new LoginDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expired = token.ValidTo
                };
            }

            throw new UnauthorizedAccessException("Name or password is wrong");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? ""));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(6),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
