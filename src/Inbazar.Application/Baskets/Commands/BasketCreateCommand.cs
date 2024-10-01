using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Entities;

namespace Inbazar.Application.Baskets.Commands;

public sealed record BasketCreateCommand(
    Guid UserId) : ICommand<Guid>;
