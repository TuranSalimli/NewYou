using Microsoft.AspNetCore.Identity;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.DTOs.AuthDTOs;
using NewYou.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NewYou.Application.Features.Commands.Auth.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDTO>
{
    private readonly UserManager<Account> _userManager;
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(UserManager<Account> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);

        var email = principal.FindFirstValue(ClaimTypes.Email)
                    ?? principal.FindFirstValue("email")
                    ?? principal.FindFirstValue(JwtRegisteredClaimNames.Email);

        if (string.IsNullOrEmpty(email))
        {
            throw new Exception("Token daxilində istifadəçi məlumatı tapılmadı.");
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new Exception("Sessiya bitib və ya Refresh Token etibarsızdır.");
        }

        var (newAccessToken, newRefreshToken) = _tokenService.CreateToken(user);

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(user);

        return new AuthResponseDTO(
            Token: newAccessToken,
            RefreshToken: newRefreshToken,
            Email: user.Email!,
            Expiry: DateTime.UtcNow.AddMinutes(60)
        );
    }
}