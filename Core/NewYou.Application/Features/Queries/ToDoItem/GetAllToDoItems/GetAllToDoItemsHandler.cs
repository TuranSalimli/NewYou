using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewYou.Application.Abstraction.Services; 
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.DTOs;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Queries.ToDoItem.GetAllToDoItems;

public class GetAllToDoItemsHandler : IRequestHandler<GetAllToDoItemsRequest, Response<IList<ToDoItemDTO>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService; 

    public GetAllToDoItemsHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Response<IList<ToDoItemDTO>>> Handle(GetAllToDoItemsRequest request, CancellationToken token)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Response<IList<ToDoItemDTO>>.Fail("İstifadəçi tapılmadı.", 401);
        }

        var readRepo = _unitOfWork.GetReadRepository<Domain.Entities.ToDoItem>();

        var todos = await readRepo.GetAllAsync(
            predicate: x => x.OwnerId == Guid.Parse(currentUserId),
            include: q => q.Include(x => x.Project),
            enableTracking: false
        );

        var mappedDtos = _mapper.Map<IList<ToDoItemDTO>>(todos);

        return Response<IList<ToDoItemDTO>>.Success(mappedDtos, 200);
    }
}