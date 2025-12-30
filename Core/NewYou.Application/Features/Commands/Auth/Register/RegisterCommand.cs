using MediatR;

namespace NewYou.Application.Features.Commands.Auth.Register;

public record RegisterCommand( string Email, string Password,string Address,string FirstName,string LastName) : IRequest<bool>;