using Inbazar.Domain.Primitives;
using MediatR;

namespace Inbazar.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
