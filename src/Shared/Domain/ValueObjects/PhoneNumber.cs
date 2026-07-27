using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    public string? Value { get; private set; }
    private PhoneNumber() { }
    private PhoneNumber(string? value) => Value = value;
    public static Result<PhoneNumber> Create(string? value)
    {
        var phoneNumberResult = Normalize(value);

        if (phoneNumberResult.IsFailure)
            return Result<PhoneNumber>.Fail(phoneNumberResult.Errors);

        return Result<PhoneNumber>.Ok(new PhoneNumber(phoneNumberResult.Data));
    }
    private static Result<string?> Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<string?>.Ok(null);

        var digits = Regex.Replace(value.Trim(), @"\D", "");

        if (digits.StartsWith("55") && digits.Length == 13)
            digits = digits[2..];

        if (!IsValid(digits))
            return Result<string?>.Fail("Número de celular inválido.");

        return Result<string?>.Ok(digits);
    }
    private static bool IsValid(string phone)
    {
        if (phone.Length != 11)
            return false;

        var ddd = phone[..2];
        var number = phone[2..];

        if (ddd.StartsWith("0"))
            return false;

        if (!number.StartsWith("9"))
            return false;

        if (phone.Distinct().Count() == 1)
            return false;

        return true;
    }
    private string? Format() => $"({Value?[..2]}) {Value?.Substring(2, 5)}-{Value?.Substring(7, 4)}";
    public override string? ToString() => Format();
    public static implicit operator string?(PhoneNumber phoneNumber) => phoneNumber.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}