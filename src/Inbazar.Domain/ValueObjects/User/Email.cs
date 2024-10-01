using Inbazar.Domain.Primitives;
using Inbazar.Domain.Shared;
using System.Text.RegularExpressions;

namespace Inbazar.Domain.ValueObjects.User;

public sealed class Email : ValueObject
{
    private Email(string value) =>
        Value = value;

    public string Value { get; }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<Email>(new Error(
                "Email.Empty", "The value can't be null or empty"));
        }

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        var result = Regex.IsMatch(value, pattern);
        if (!result)
        {
            return Result.Failure<Email>(new Error(
                "Email.Invalid", "The specified value is not valid"));
        }

        return new Email(value);
    }
}
