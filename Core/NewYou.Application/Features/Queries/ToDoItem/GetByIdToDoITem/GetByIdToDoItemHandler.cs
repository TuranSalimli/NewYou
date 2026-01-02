using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.DTOs;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Queries.ToDoItem.GetByIdToDoItem;

public class GetByIdToDoItemHandler : IRequestHandler<GetByIdToDoItemRequest, Response<ToDoItemDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetByIdToDoItemHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Response<ToDoItemDTO>> Handle(GetByIdToDoItemRequest request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Response<ToDoItemDTO>.Fail("İstifadəçi tapılmadı.", 401);
        }

        var readRepo = _unitOfWork.GetReadRepository<Domain.Entities.ToDoItem>();

        var todo = await readRepo.GetAsync(
            predicate: x => x.Id == request.Id && x.OwnerId == Guid.Parse(currentUserId),
            include: q => q.Include(x => x.Project),
            enableTracking: false
        );

        if (todo == null)
        {
            return Response<ToDoItemDTO>.Fail("Tapşırıq tapılmadı və ya giriş icazəniz yoxdur.", 404);
        }

        var dto = _mapper.Map<ToDoItemDTO>(todo);
        return Response<ToDoItemDTO>.Success(dto, 200);
    }
}