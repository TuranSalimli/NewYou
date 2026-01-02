using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.DTOs.ProjectDTOs;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Queries.Project.GetProjectById;

public class GetByIdProjectHandler : IRequestHandler<GetByIdProjectRequest, Response<ProjectDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetByIdProjectHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Response<ProjectDTO>> Handle(GetByIdProjectRequest request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Response<ProjectDTO>.Fail("İstifadəçi tapılmadı.", 401);
        }

        var readRepo = _unitOfWork.GetReadRepository<Domain.Entities.Project>();

        var project = await readRepo.GetAsync(
            predicate: x => x.Id == request.Id && x.OwnerId == Guid.Parse(currentUserId),
            include: q => q.Include(x => x.ToDoItems), 
            enableTracking: false
        );

        if (project == null)
        {
            return Response<ProjectDTO>.Fail("Layihə tapılmadı və ya bu layihəni görmək üçün icazəniz yoxdur.", 404);
        }

        var dto = _mapper.Map<ProjectDTO>(project);

        return Response<ProjectDTO>.Success(dto, 200);
    }
}