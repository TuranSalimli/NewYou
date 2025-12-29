using NewYou.Application.DTOs.AuthDTOs;

namespace NewYou.Application.Features.Commands.Auth.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDTO>;
