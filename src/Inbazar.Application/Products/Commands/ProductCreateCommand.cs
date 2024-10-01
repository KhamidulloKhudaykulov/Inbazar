using Inbazar.Application.Abstractions.Messaging;

namespace Inbazar.Application.Products.Commands;

public record ProductCreateCommand(
    string Name,
    string Description,
    Guid CategoryId,
    decimal Price) : ICommand;
