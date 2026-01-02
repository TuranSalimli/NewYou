using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewYou.Application.Features.Commands.Auth.ForgetPassword;
using NewYou.Application.Features.Commands.Auth.Login;
using NewYou.Application.Features.Commands.Auth.RefreshToken;
using NewYou.Application.Features.Commands.Auth.Register;
using NewYou.Application.Features.Commands.Auth.ResetPassword;
using NewYou.Application.Features.Commands.Auth.VerifyEmail;

namespace NewYou.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { Message = "Qeydiyyat uğurla tamamlandı!" });
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { Message = "Hesabınız uğurla təsdiqləndi! İndi login ola bilərsiniz." });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        await _mediator.Send(command);
        return Ok(new { message = "Əgər email sistemdə varsa, sıfırlama kodu göndərildi." });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { message = "Şifrəniz uğurla yeniləndi." });
    }
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}