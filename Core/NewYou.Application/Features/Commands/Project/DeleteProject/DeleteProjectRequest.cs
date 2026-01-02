using MediatR;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Commands.Project.DeleteProject;

public class DeleteProjectRequest : IRequest<Response<bool>>
{
    public Guid Id { get; set; }

    public DeleteProjectRequest() { }

    public DeleteProjectRequest(Guid id)
    {
        Id = id;
    }
}