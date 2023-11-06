using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Domain.Abstract;

public interface IJwtTokenService
{
    JwtSecurityToken GetToken(List<Claim> authClaims);
}