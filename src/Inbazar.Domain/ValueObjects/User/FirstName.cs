using Inbazar.Domain.Primitives;
using Inbazar.Domain.Shared;

namespace Inbazar.Domain.ValueObjects.User;

public sealed class FirstName : ValueObject
{
    public const int MaxLenght = 50;
    private FirstName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<FirstName> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<FirstName>(new Error(
                "FirstName.Empty", "The value can't be null or empty"));
        }

        if (value.Length > MaxLenght)
        {
            return Result.Failure<FirstName>(new Error(
                "FirstName.TooLong",
                "First name is too long."));
        }

        return new FirstName(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
