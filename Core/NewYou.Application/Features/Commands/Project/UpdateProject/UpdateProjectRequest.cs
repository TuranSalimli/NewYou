using MediatR;
using NewYou.Application.GenericResponses;

namespace NewYou.Application.Features.Commands.Project.UpdateProject;

public class UpdateProjectRequest : IRequest<Response<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ColorHex { get; set; }
}