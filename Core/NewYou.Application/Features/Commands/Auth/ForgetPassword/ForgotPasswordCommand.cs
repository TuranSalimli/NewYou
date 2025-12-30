namespace NewYou.Application.Features.Commands.Auth.ForgetPassword;
public record ForgotPasswordCommand(string Email) : IRequest<bool>;
