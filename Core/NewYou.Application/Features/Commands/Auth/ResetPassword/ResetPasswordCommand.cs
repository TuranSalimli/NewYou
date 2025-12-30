namespace NewYou.Application.Features.Commands.Auth.ResetPassword;
public record ResetPasswordCommand(string Email,string Token,string NewPassword,string ConfirmPassword) : IRequest<bool>;