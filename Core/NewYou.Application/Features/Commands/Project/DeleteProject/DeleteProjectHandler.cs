using MediatR;
using NewYou.Application.Abstraction.Services;
using NewYou.Application.Abstraction.UnitOfWorks;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Commands.Project.DeleteProject;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, Response<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public DeleteProjectHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Response<bool>> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (string.IsNullOrEmpty(userId))
            return Response<bool>.Fail("Giriş icazəniz yoxdur.", 401);

        var readRepo = _unitOfWork.GetReadRepository<NewYou.Domain.Entities.Project>();
        var writeRepo = _unitOfWork.GetWriteRepository<NewYou.Domain.Entities.Project>();

        var project = await readRepo.GetAsync(x => x.Id == request.Id && x.OwnerId == Guid.Parse(userId));

        if (project == null)
        {
            return Response<bool>.Fail("Silinəcək layihə tapılmadı və ya icazəniz yoxdur.", 404);
        }

        writeRepo.HardDeleteAsync(project);

        int result = await _unitOfWork.SaveAsync();

        if (result > 0)
            return Response<bool>.Success(true, 200);

        return Response<bool>.Fail("Silinmə zamanı bazada xəta baş verdi.", 500);
    }
}