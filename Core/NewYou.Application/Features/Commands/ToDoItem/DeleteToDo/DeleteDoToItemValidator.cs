using FluentValidation;

namespace NewYou.Application.Features.Commands.ToDoItem.DeleteToDo;

public class DeleteToDoItemValidator : AbstractValidator<DeleteToDoItemRequest>
{
    public DeleteToDoItemValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Silinəcək elementin ID-si mütləqdir.")
            .NotEqual(Guid.Empty).WithMessage("Düzgün bir ID daxil edin.");
    }
}