namespace NewYou.Application.Abstraction.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
}