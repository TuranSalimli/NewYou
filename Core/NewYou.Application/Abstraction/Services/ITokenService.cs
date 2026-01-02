using System.Security.Claims; 
using NewYou.Domain.Entities;

namespace NewYou.Application.Abstraction.Services;

public interface ITokenService
{
    (string accessToken, string refreshToken) CreateToken(Account user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}