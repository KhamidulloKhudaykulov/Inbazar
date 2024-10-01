using Inbazar.Application.Abstractions.Messaging;

namespace Inbazar.Application.Orders.Commands;

public record OrderCreateCommand(
    Guid UserId,
    Guid BasketId) : ICommand;
