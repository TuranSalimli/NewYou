using NewYou.Application.GenericResponses;

public class CreateProjectRequest : IRequest<Response<Guid>>
{
    public string Name { get; set; }
    public string? ColorHex { get; set; }
}