using MediatR;
using Microsoft.AspNetCore.Identity;
using NewYou.Domain.Entities;

namespace NewYou.Application.Features.Commands.Auth.VerifyEmail;

public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, bool>
{
    private readonly UserManager<Account> _userManager;

    public VerifyEmailCommandHandler(UserManager<Account> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new Exception("İstifadəçi tapılmadı.");

        var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", request.Code);

        if (!result)
            throw new Exception("Təsdiqləmə kodu yanlışdır və ya vaxtı bitib.");

        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);

        return true;
    }
}