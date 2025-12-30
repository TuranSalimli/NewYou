using MediatR;
using Microsoft.AspNetCore.Identity;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Features.Commands.Auth.ForgetPassword;
using NewYou.Domain.Entities;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace NewYou.Application.Features.Commands.Auth.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, bool>
{
    private readonly UserManager<Account> _userManager;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(UserManager<Account> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }


public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
{
    var user = await _userManager.FindByEmailAsync(request.Email);
    if (user == null) return true;

    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

    byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
    var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

    string frontendUrl = "http://localhost:3000/reset-password";
    string resetLink = $"{frontendUrl}?token={encodedToken}&email={user.Email}";

    var message = $@"
        <h3>Şifrə Sıfırlama İstəyi</h3>
        <p>Salam {user.FirstName},</p>
        <p>Şifrənizi yeniləmək üçün aşağıdakı linkə klikləyin:</p>
        <a href='{resetLink}'>Şifrəni Yenilə</a>
        <br/><br/>
        <b>Kodunuz (əgər lazım olsa):</b> {token}";

    await _emailService.SendEmailAsync(user.Email!, "Şifrə Sıfırlama İstəyi", message);

    return true;
}
} 