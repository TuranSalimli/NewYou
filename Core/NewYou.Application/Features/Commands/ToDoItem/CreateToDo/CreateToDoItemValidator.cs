
namespace NewYou.Application.Features.Commands.ToDoItem.CreateToDo;

public class CreateToDoItemValidator : AbstractValidator<CreateToDoItemRequest>
{
    public CreateToDoItemValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlıq boş ola bilməz.")
            .MinimumLength(3).WithMessage("Başlıq ən azı 3 simvoldan ibarət olmalıdır.")
            .MaximumLength(150).WithMessage("Başlıq 150 simvoldan çox ola bilməz.");

        RuleFor(x => x.Note)
            .MaximumLength(500).WithMessage("Qeyd 500 simvoldan çox ola bilməz.");

        RuleFor(x => x.Deadline)
            .Must(date => !date.HasValue || date.Value > DateTime.UtcNow)
            .WithMessage("Deadline keçmiş tarix ola bilməz.");

        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("Bir layihə seçməlisiniz.");

    }
}