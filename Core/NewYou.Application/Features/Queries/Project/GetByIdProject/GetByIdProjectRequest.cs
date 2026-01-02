using MediatR;
using NewYou.Application.DTOs;
using NewYou.Application.DTOs.ProjectDTOs;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Queries.Project.GetProjectById;

public class GetByIdProjectRequest : IRequest<Response<ProjectDTO>>
{
    public Guid Id { get; set; }

    public GetByIdProjectRequest() { }

    public GetByIdProjectRequest(Guid id)
    {
        Id = id;
    }
}