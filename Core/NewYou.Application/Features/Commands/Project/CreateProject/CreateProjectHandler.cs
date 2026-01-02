using AutoMapper;
using MediatR;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.GenericResponses;
using NewYou.Domain.Entities;

namespace NewYou.Application.Features.Commands.Project.CreateProject;

public class CreateProjectHandler : IRequestHandler<CreateProjectRequest, Response<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public CreateProjectHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Response<Guid>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            return Response<Guid>.Fail("İstifadəçi tapılmadı. Zəhmət olmasa yenidən giriş edin.", 401);
        }

        var writeRepository = _unitOfWork.GetWriteRepository<NewYou.Domain.Entities.Project>();

        var project = _mapper.Map<NewYou.Domain.Entities.Project>(request);

        project.OwnerId = Guid.Parse(userId);
        project.CreateData = DateTime.UtcNow; 

        await writeRepository.CreateAsync(project);

        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
        {
            return Response<Guid>.Success(project.Id, 201);
        }

        return Response<Guid>.Fail("Layihəni yaradarkən xəta baş verdi.", 500);
    }
}