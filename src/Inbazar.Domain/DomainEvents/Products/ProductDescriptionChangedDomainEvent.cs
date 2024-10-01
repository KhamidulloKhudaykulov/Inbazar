namespace Inbazar.Domain.DomainEvents.Products;

public sealed record ProductDescriptionChangedDomainEvent(Guid Id, Guid ProductId) : DomainEvent(Id);
