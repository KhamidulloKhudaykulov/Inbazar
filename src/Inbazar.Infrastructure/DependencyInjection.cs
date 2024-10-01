using Inbazar.Infrastructure.BackgroundJobs;
using Inbazar.Infrastructure.Idempotents;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Inbazar.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddQuartz(cfg =>
        {
            var jobkey = new JobKey(nameof(ProcessOutboxMessagesJob));

            cfg
                .AddJob<ProcessOutboxMessagesJob>(jobkey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobkey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInSeconds(10)
                                        .RepeatForever()));

            cfg.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));
        return services;
    }
}
