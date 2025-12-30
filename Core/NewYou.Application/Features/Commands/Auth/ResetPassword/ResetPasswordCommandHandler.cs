using MediatR;
using Microsoft.AspNetCore.Identity;
using NewYou.Domain.Entities;

namespace NewYou.Application.Features.Commands.Auth.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly UserManager<Account> _userManager;

    public ResetPasswordCommandHandler(UserManager<Account> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.NewPassword != request.ConfirmPassword)
            throw new Exception("Şifrələr bir-biri ilə eyni deyil.");

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new Exception("İstifadəçi tapılmadı.");

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!result.Succeeded)
        {
            var firstError = result.Errors.First().Description;
            throw new Exception($"Xəta baş verdi: {firstError}");
        }

        return true;
    }
}