using Inbazar.Domain.Primitives;

namespace Inbazar.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
