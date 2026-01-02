using AutoMapper;
using MediatR;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.GenericResponses; 
using NewYou.Domain.Entities;

namespace NewYou.Application.Features.Commands.ToDoItem.CreateToDo;

public class CreateToDoItemHandler : IRequestHandler<CreateToDoItemRequest, Response<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
   private readonly ICurrentUserService _currentUserService;
    public CreateToDoItemHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Response<Guid>> Handle(CreateToDoItemRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            return Response<Guid>.Fail("İstifadəçi tapılmadı. Zəhmət olmasa yenidən giriş edin.", 401);
        }
        var writeRepository = _unitOfWork.GetWriteRepository<Domain.Entities.ToDoItem>();

        var todoItem = _mapper.Map<Domain.Entities.ToDoItem>(request);
        
        todoItem.OwnerId = Guid.Parse(userId);

        await writeRepository.CreateAsync(todoItem);

        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
        {
            return Response<Guid>.Success(todoItem.Id, 201);
        }

        return Response<Guid>.Fail("Məlumatı bazaya qeyd edərkən xəta baş verdi.", 500);
    }
}