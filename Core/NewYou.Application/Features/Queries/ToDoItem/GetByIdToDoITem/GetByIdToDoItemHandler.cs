using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.DTOs;
using NewYou.Application.Features.Queries.ToDoItem.GetByIdToDoItem;
using NewYou.Application.GenericResponses;
using NewYou.Domain.Entities;

public class GetByIdToDoItemHandler : IRequestHandler<GetByIdToDoItemRequest, Response<ToDoItemDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdToDoItemHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<ToDoItemDTO>> Handle(GetByIdToDoItemRequest request, CancellationToken cancellationToken)
    {
        var readRepo = _unitOfWork.GetReadRepository<ToDoItem>();

        var todo = await readRepo.GetAsync(
            predicate: x => x.Id == request.Id,
            include: q => q.Include(x => x.Project),
            enableTracking: false
        );

        if (todo == null)
        {
            return Response<ToDoItemDTO>.Fail("Belə bir ToDo tapılmadı.", 404);
        }

        var dto = _mapper.Map<ToDoItemDTO>(todo);
        return Response<ToDoItemDTO>.Success(dto, 200);
    }
}