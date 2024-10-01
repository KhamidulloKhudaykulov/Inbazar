using Inbazar.Domain.DomainEvents.Products;
using Inbazar.Domain.Primitives;
using Inbazar.Domain.ValueObjects.Product;

namespace Inbazar.Domain.Entities;

public class Product : AggregateRoot
{
    private Product(
        Guid id,
        ProductName name,
        Description description,
        Price price) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public ProductName? Name { get; private set; }
    public Description? Description { get; private set; }
    public Price? Price { get; private set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }

    public static Product Create(
        Guid id,
        ProductName name,
        Description description,
        Price price)
    {
        return new Product(id, name, description, price);
    }

    public bool Equals(Product? other)
    {
        if (other is null)
            return false;

        if (other.GetType() != this.GetType())
            return false;

        return other.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 43;
    }

    public void ChangeName(ProductName value)
    {
        Name = value;
        RaiseDomainEvent(new ProductNameChangedDomainEvent(
            Guid.NewGuid(),
            Id));
    }

    public void ChangeDescription(Description value)
    {
        Description = value;
        RaiseDomainEvent(new ProductDescriptionChangedDomainEvent(
            Guid.NewGuid(),
            Id));
    }
}
