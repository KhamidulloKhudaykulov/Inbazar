using Inbazar.Domain.ValueObjects.Category;

namespace Inbazar.Domain.Entities;

public class Category : IEquatable<Category>
{
    private Category(
        Guid id,
        CategoryName name,
        Guid subCategory)
    {
        Id = id;
        Name = name;
        SubCategory = subCategory;
    }

    public Guid Id { get; private set; }
    public CategoryName? Name { get; private set; }
    public Guid SubCategory { get; private set; } = Guid.Empty;
    public ICollection<ProductCategory>? ProductCategories { get; set; }

    public static Category Create(
        CategoryName name, Guid subCategory)
    {
        if (subCategory == Guid.Empty)
        {
            return new Category(
            Guid.NewGuid(),
            name,
            Guid.Empty);
        }

        return new Category(
            Guid.NewGuid(),
            name,
            subCategory);
    }

    public bool Equals(Category? other)
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
}
