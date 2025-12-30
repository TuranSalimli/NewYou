namespace NewYou.Application.DTOs.AuthDTOs;

public record AuthResponseDTO(string Token,string RefreshToken,string Email,DateTime Expiry);
