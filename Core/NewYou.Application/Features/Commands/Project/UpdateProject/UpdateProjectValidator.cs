using FluentValidation;
using NewYou.Application.Features.Commands.Project.UpdateProject;

namespace NewYou.Application.Validators.Project;

public class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Layihə ID-si boş ola bilməz.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Layihə adı mütləq doldurulmalıdır.")
            .MinimumLength(3).WithMessage("Layihə adı ən azı 3 simvol olmalıdır.")
            .MaximumLength(100).WithMessage("Layihə adı 100 simvoldan çox olmamalıdır.");

        RuleFor(x => x.ColorHex)
            .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$")
            .WithMessage("Rəng kodu düzgün deyil (Məsələn: #FFFFFF və ya #000).")
            .When(x => !string.IsNullOrEmpty(x.ColorHex));
    }
}