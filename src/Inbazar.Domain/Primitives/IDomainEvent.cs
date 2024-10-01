using MediatR;

namespace Inbazar.Domain.Primitives;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
}
