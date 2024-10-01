using Inbazar.Domain.Primitives;
using Inbazar.Domain.Shared;

namespace Inbazar.Domain.ValueObjects.Product;

public class ProductName : ValueObject
{
    public const int Max_Lenght = 50;
    private ProductName(string value)
        => Value = value;

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<ProductName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<ProductName>(new Error(
                "ProductName.NullValue", "The specified value can't be null or empty"));
        }

        if (value.Length > Max_Lenght)
        {
            return Result.Failure<ProductName>(new Error(
                "ProductName.TooLong", "Product name is too long"));
        }

        return new ProductName(value);
    }
}
