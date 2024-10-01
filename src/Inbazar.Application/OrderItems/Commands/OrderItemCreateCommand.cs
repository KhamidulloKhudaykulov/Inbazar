using Inbazar.Application.Abstractions.Messaging;

namespace Inbazar.Application.OrderItems.Commands;

public record OrderItemCreateCommand(
    Guid UserId,
    Guid ProductId,
    int Amount) : ICommand;
