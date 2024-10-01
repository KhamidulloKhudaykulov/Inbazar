using Inbazar.Domain.Primitives;
using Inbazar.Domain.Shared;

namespace Inbazar.Domain.ValueObjects.Product;

public class Description : ValueObject
{
    public const int Max_Lenght = 50;
    private Description(string value)
        => Value = value;

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<Description> Create(string value)
    {
        return new Description(value);
    }
}
