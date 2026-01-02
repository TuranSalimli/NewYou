using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.DTOs.ProjectDTOs;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Queries.Project.GetAllProjects;

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsRequest, Response<IList<ProjectDTO>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAllProjectsHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Response<IList<ProjectDTO>>> Handle(GetAllProjectsRequest request, CancellationToken token)
    {
        var currentUserId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Response<IList<ProjectDTO>>.Fail("İstifadəçi tapılmadı. Zəhmət olmasa yenidən giriş edin.", 401);
        }

        var readRepo = _unitOfWork.GetReadRepository<NewYou.Domain.Entities.Project>();

        var projects = await readRepo.GetAllAsync(
            predicate: x => x.OwnerId == Guid.Parse(currentUserId),
             include: q => q.Include(x => x.ToDoItems),
            enableTracking: false
        );


        var mappedDtos = _mapper.Map<IList<ProjectDTO>>(projects);

        return Response<IList<ProjectDTO>>.Success(mappedDtos, 200);
    }
}