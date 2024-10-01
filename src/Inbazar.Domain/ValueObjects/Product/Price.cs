using Inbazar.Domain.Primitives;
using Inbazar.Domain.Shared;

namespace Inbazar.Domain.ValueObjects.Product;

public class Price : ValueObject
{
    private Price(decimal value)
        => Value = value;

    public decimal Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<Price> Create(decimal value)
    {
        try
        {
            return new Price(value);
        }
        catch (Exception ex)
        {
            return Result.Failure<Price>(new Error(
                code: "Price.Invalid",
                message: ex.Message.ToString()
                ));
        }
    }
}
