using Inbazar.Application.Abstractions.Messaging;

namespace Inbazar.Application.Users.Commands;

public record UserUpdateCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string Password) : ICommand;
