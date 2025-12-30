using FluentValidation;

namespace NewYou.Application.Features.Commands.Auth.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz.")
            .EmailAddress().WithMessage("Düzgün bir email ünvanı daxil edin.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz.")
            .MinimumLength(6).WithMessage("Şifrə ən azı 6 simvoldan ibarət olmalıdır.")
            .Matches(@"[A-Z]").WithMessage("Şifrədə ən azı bir böyük hərf olmalıdır.")
            .Matches(@"[a-z]").WithMessage("Şifrədə ən azı bir kiçik hərf olmalıdır.")
            .Matches(@"[0-9]").WithMessage("Şifrədə ən azı bir rəqəm olmalıdır.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad boş ola bilməz.")
            .MaximumLength(50).WithMessage("Ad 50 simvoldan çox ola bilməz.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad boş ola bilməz.")
            .MaximumLength(50).WithMessage("Soyad 50 simvoldan çox ola bilməz.");
    }
}