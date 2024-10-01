using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.DomainEvents;

namespace Inbazar.Application.Users.Events;

internal sealed class UserCreatedDomainEventHandler
    : IDomainEventHandler<UserCreatedDomainEvent>
{
    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // Business logic
        Console.WriteLine("Worked");
        await Task.CompletedTask;
    }
}
