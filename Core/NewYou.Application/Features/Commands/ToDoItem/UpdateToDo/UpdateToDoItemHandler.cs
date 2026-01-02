using AutoMapper;
using MediatR;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.GenericResponses;
using NewYou.Domain.Entities;

namespace NewYou.Application.Features.Commands.ToDoItem.UpdateToDo;

public class UpdateToDoItemHandler : IRequestHandler<UpdateToDoItemRequest, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService; 

    public UpdateToDoItemHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Response<bool>> Handle(UpdateToDoItemRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(userId))
            return Response<bool>.Fail("Giriş icazəniz yoxdur.", 401);

        var readRepo = _unitOfWork.GetReadRepository<Domain.Entities.ToDoItem>();
        var writeRepo = _unitOfWork.GetWriteRepository<Domain.Entities.ToDoItem>();

        var todoItem = await readRepo.GetAsync(x => x.Id == request.Id && x.OwnerId == Guid.Parse(userId));

        if (todoItem == null)
        {

            return Response<bool>.Fail("Yenilənəcək ToDo tapılmadı və ya icazəniz yoxdur.", 404);
        }

        _mapper.Map(request, todoItem);

        await writeRepo.UpdateAsync(todoItem);
        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
            return Response<bool>.Success(true, 200);

        return Response<bool>.Fail("Yadda saxlanarkən xəta baş verdi.", 500);
    }
}