using AutoMapper;
using MediatR;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.GenericResponses; 
using NewYou.Domain.Entities;

namespace NewYou.Application.Features.Commands.ToDoItem.CreateToDo;

public class CreateToDoItemHandler : IRequestHandler<CreateToDoItemRequest, Response<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateToDoItemHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<Guid>> Handle(CreateToDoItemRequest request, CancellationToken cancellationToken)
    {
        var writeRepository = _unitOfWork.GetWriteRepository<Domain.Entities.ToDoItem>();

        var todoItem = _mapper.Map<Domain.Entities.ToDoItem>(request);

        await writeRepository.CreateAsync(todoItem);

        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
        {
            return Response<Guid>.Success(todoItem.Id, 201);
        }

        return Response<Guid>.Fail("Məlumatı bazaya qeyd edərkən xəta baş verdi.", 500);
    }
}