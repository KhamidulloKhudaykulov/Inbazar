using Inbazar.Domain.Primitives;
using Inbazar.Persistence;
using Inbazar.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Quartz;

namespace Inbazar.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(IPublisher publisher, ApplicationDbContext dbContext)
    {
        _publisher = publisher;
        _dbContext = dbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var message = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in message)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                outboxMessage.Content,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

            if (domainEvent is null) continue;

            var policy = Policy
                .Handle<Exception>()
                .RetryAsync(3);

            var result = await policy.ExecuteAndCaptureAsync(() =>
                _publisher.Publish(
                    domainEvent,
                    context.CancellationToken));

            outboxMessage.Error = result.FinalException?.ToString();
            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}
