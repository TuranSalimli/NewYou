using NewYou.Application.DTOs.AuthDTOs;

namespace NewYou.Application.Features.Commands.Auth.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<AuthResponseDTO>;