using Inbazar.Domain.Primitives;
using Inbazar.Domain.Shared;

namespace Inbazar.Domain.ValueObjects.User;

public sealed class LastName : ValueObject
{
    public const int MaxLenght = 50;
    private LastName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<LastName>(new Error(
                "LastName.Empty", "The value can't be null or empty"));
        }

        if (value.Length > MaxLenght)
        {
            return Result.Failure<LastName>(new Error(
                "LastName.TooLong", "Last name is too long"));
        }

        return new LastName(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
