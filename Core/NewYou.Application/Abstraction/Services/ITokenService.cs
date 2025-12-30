using NewYou.Domain.Entities;

namespace NewYou.Application.Abstraction.Services;

public interface ITokenService
{
    (string accessToken, string refreshToken) CreateToken(Account user);
}