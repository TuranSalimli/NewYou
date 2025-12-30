using MediatR;

namespace NewYou.Application.Features.Commands.Auth.VerifyEmail;

public record VerifyEmailCommand(string Email, string Code) : IRequest<bool>;