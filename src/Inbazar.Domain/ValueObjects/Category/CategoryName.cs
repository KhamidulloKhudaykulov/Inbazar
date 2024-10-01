using Inbazar.Domain.Primitives;
using Inbazar.Domain.Shared;

namespace Inbazar.Domain.ValueObjects.Category;

public class CategoryName : ValueObject
{
    public const int MaxLenght = 15;
    private CategoryName(string value)
        => Value = value;

    public string Value { get; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<CategoryName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<CategoryName>(new Error(
                code: "CategoryName.NullValue",
                message: "The specified value can't be null or empty"
                ));
        }

        if (value.Length > MaxLenght)
        {
            return Result.Failure<CategoryName>(new Error(
                code: "CategoryName.TooLong",
                message: "Category name is too long"));
        }

        return new CategoryName(value);
    }
}
