using FluentValidation;
using NewYou.Application.Features.Commands.Project.DeleteProject;

namespace NewYou.Application.Validators.Project;

public class DeleteProjectValidator : AbstractValidator<DeleteProjectRequest>
{
    public DeleteProjectValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Silinəcək layihənin ID-si mütləqdir.")
            .NotEqual(Guid.Empty).WithMessage("Keçərli bir ID daxil edilməlidir.");
    }
}