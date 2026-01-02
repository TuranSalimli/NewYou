using FluentValidation;

namespace NewYou.Application.Features.Commands.ToDoItem.UpdateToDo;

public class UpdateToDoItemValidator : AbstractValidator<UpdateToDoItemRequest>
{
    public UpdateToDoItemValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Yenilənəcək qeydin ID-si mütləqdir.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlıq boş ola bilməz.")
            .MinimumLength(3).WithMessage("Başlıq ən azı 3 simvol olmalıdır.")
            .MaximumLength(150).WithMessage("Başlıq 150 simvoldan çox ola bilməz.");

        RuleFor(x => x.Note)
            .MaximumLength(500).WithMessage("Qeyd hissəsi 500 simvoldan çox ola bilməz.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Düzgün status seçilməyib.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Düzgün prioritet seçilməyib.");

        RuleFor(x => x.Deadline)
            .Must(date => !date.HasValue || date.Value > DateTime.Now)
            .WithMessage("Deadline keçmiş bir tarix ola bilməz.");
    }
}