namespace NewYou.Application.Features.Commands.Auth.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email ünvanını daxil edin.")
            .EmailAddress().WithMessage("Düzgün bir email ünvanı daxil edin.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrəni daxil edin.")
            .MinimumLength(6).WithMessage("Şifrə ən azı 6 simvoldan ibarət olmalıdır.");
    }
}