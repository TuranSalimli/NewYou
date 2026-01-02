using MediatR;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Commands.Project.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest, Response<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public UpdateProjectHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Response<Guid>> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            return Response<Guid>.Fail("İstifadəçi tapılmadı.", 401);
        }

        var readRepository = _unitOfWork.GetReadRepository<NewYou.Domain.Entities.Project>();
        var writeRepository = _unitOfWork.GetWriteRepository<NewYou.Domain.Entities.Project>();

        var project = await readRepository.GetAsync(x => x.Id == request.Id && x.OwnerId == Guid.Parse(userId));

        if (project == null)
        {
            return Response<Guid>.Fail("Yenilənəcək layihə tapılmadı və ya bu əməliyyat üçün icazəniz yoxdur.", 404);
        }

        project.Name = request.Name;
        project.ColorHex = request.ColorHex;

        writeRepository.UpdateAsync(project);

        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
        {
            return Response<Guid>.Success(project.Id, 200);
        }

        return Response<Guid>.Fail("Məlumatı bazada yeniləyərkən xəta baş verdi.", 500);
    }
}