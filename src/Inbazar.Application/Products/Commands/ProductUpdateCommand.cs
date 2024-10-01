using Inbazar.Application.Abstractions.Messaging;

namespace Inbazar.Application.Products.Commands;

public sealed record ProductUpdateCommand(
    Guid Id,
    string? Name,
    string? Description,
    Guid? CategoryId,
    int Amount,
    decimal Price) : ICommand;
