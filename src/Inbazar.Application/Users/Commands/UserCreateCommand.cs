using Inbazar.Application.Abstractions.Messaging;

namespace Inbazar.Application.Users.Commands;

public record UserCreateCommand(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Password) : ICommand<UserCreateCommand>;
