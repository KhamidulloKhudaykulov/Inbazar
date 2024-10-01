using Inbazar.Application.Abstractions.Messaging;

namespace Inbazar.Application.OrderItems.Commands;

public record ChangeProductAmountCommand(
    Guid OrderItemId,
    Guid BasketId,
    bool up,
    int amount
    )
    : ICommand;
