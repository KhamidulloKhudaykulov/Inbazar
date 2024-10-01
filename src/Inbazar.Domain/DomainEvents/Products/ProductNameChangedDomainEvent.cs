namespace Inbazar.Domain.DomainEvents.Products;

public sealed record ProductNameChangedDomainEvent(Guid Id, Guid ProductId) : DomainEvent(Id);
