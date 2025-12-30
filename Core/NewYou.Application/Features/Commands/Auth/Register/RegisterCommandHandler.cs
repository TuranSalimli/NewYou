using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Features.Commands.Auth.Register;
using NewYou.Domain.Entities;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly UserManager<Account> _userManager;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public RegisterCommandHandler(UserManager<Account> userManager, IMapper mapper, IEmailService emailService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _emailService = emailService;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Account>(request);
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));
        }


        var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

        string mailBody = $@"
            <div style='font-family: sans-serif; padding: 20px; border: 1px solid #eee;'>
                <h2>NewYou-ya Xoş Gəldiniz!</h2>
                <p>Qeydiyyatı tamamlamaq üçün təsdiqləmə kodunuz:</p>
                <h1 style='color: #4A90E2; letter-spacing: 5px;'>{code}</h1>
                <p>Bu kodu heç kimlə paylaşmayın.</p>
            </div>";

        await _emailService.SendEmailAsync(user.Email!, "Hesab Təsdiqləmə Kodu", mailBody);

        return true;
    }
}