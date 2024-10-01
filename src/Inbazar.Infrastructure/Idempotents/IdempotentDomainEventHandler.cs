using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Primitives;
using Inbazar.Persistence;
using Inbazar.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inbazar.Infrastructure.Idempotents;

public sealed class IdempotentDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly INotificationHandler<TDomainEvent> _decorated;
    private readonly ApplicationDbContext _dbContext;

    public IdempotentDomainEventHandler(
        INotificationHandler<TDomainEvent> decorated,
        ApplicationDbContext dbContext)
    {
        _decorated = decorated;
        _dbContext = dbContext;
    }

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        string consumer = _decorated.GetType().Name;

        if (await _dbContext.Set<OutboxMessageConsumer>()
                .AnyAsync(
                    outboxMessageConsumer =>
                        outboxMessageConsumer.Id == notification.Id &&
                        outboxMessageConsumer.Name == consumer,
                    cancellationToken))
        {
            return;
        }

        await _decorated.Handle(notification, cancellationToken);

        _dbContext.Set<OutboxMessageConsumer>()
            .Add(new OutboxMessageConsumer
            {
                Id = notification.Id,
                Name = consumer,
            });

        await _dbContext.SaveChangesAsync();
    }
}
