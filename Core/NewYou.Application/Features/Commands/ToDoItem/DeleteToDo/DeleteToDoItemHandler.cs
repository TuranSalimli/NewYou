using MediatR;
using NewYou.Application.Abstraction.Services; 
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.Features.Commands.ToDoItem.DeleteToDo;
using NewYou.Application.GenericResponses;
using NewYou.Domain.Entities;

public class DeleteToDoItemHandler : IRequestHandler<DeleteToDoItemRequest, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService; 

    public DeleteToDoItemHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Response<bool>> Handle(DeleteToDoItemRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId; 

        if (string.IsNullOrEmpty(userId))
            return Response<bool>.Fail("Giriş icazəniz yoxdur.", 401);

        var readRepo = _unitOfWork.GetReadRepository<ToDoItem>();
        var writeRepo = _unitOfWork.GetWriteRepository<ToDoItem>();

        var todoItem = await readRepo.GetAsync(x => x.Id == request.Id && x.OwnerId == Guid.Parse(userId));

        if (todoItem == null)
        {
            return Response<bool>.Fail("Məlumat tapılmadı və ya silmə icazəniz yoxdur.", 404);
        }

        await writeRepo.HardDeleteAsync(todoItem);
        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
            return Response<bool>.Success(true, 200);

        return Response<bool>.Fail("Silinmə zamanı xəta baş verdi.", 500);
    }
}