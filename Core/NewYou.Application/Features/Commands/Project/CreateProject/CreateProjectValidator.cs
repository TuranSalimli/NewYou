using FluentValidation;

namespace NewYou.Application.Features.Commands.Project.CreateProject;

public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Layihə adı boş ola bilməz.")
            .MaximumLength(100).WithMessage("Layihə adı 100 simvoldan çox ola bilməz.");

        RuleFor(x => x.ColorHex)
            .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$")
            .WithMessage("Düzgün rəng kodu daxil edin (məs: #FFFFFF).")
            .When(x => !string.IsNullOrEmpty(x.ColorHex));
    }
}