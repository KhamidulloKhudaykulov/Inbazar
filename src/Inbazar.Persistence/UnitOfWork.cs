using Inbazar.Domain.Primitives;
using Inbazar.Domain.Repositories;
using Inbazar.Persistence.Outbox;
using Newtonsoft.Json;

namespace Inbazar.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        ConvertDomainEventsToOutboxMessage();
        await _context.SaveChangesAsync(token);
    }

    private void ConvertDomainEventsToOutboxMessage()
    {
        var outboxMessages = _context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.GetDomainEvents();

                aggregateRoot.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            })
            .ToList();

        _context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}
