namespace NewYou.Application.DTOs.AuthDTOs;

public record AuthResponseDTO
    (
   string Token,
   string Email,
   DateTime Expiry
    );
