using MediatR;
using Microsoft.AspNetCore.Identity;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.DTOs.AuthDTOs;
using NewYou.Domain.Entities;
using NewYou.Domain.Exceptions;

namespace NewYou.Application.Features.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDTO>
{
    private readonly UserManager<Account> _userManager;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<Account> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new LoginFailedException("Email və ya şifrə yanlışdır.");
        }

        var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!checkPassword)
        {
            throw new LoginFailedException("Email və ya şifrə yanlışdır.");
        }

        var token = _tokenService.CreateToken(user);

        return new AuthResponseDTO(
            Token: token,
            Email: user.Email!,
            Expiry: DateTime.UtcNow.AddMinutes(60) 
        );
    }
}