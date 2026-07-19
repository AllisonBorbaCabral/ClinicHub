using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class PhoneNumber
{
    public string Value { get; private set; } = null!;
    private PhoneNumber() { } // EF Core
    private PhoneNumber(string value)
    {
        Value = value;
    }
    public static Result<PhoneNumber> Create(string phone)
    {
        var normalized = Normalize(phone);

        if (!IsValid(normalized))
            return Result<PhoneNumber>.Fail("Número de celular inválido.");

        return Result<PhoneNumber>.Ok(new PhoneNumber(normalized));
    }
    private static string Normalize(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return string.Empty;

        var digits = Regex.Replace(phone, @"\D", "");

        if (digits.StartsWith("55") && digits.Length == 13)
            digits = digits[2..];

        return digits;
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
    private string Formatted()
    {
        return $"({Value[..2]}) {Value.Substring(2, 5)}-{Value.Substring(7, 4)}";
    }
    public override string ToString()
    {
        return Formatted();
    }
}