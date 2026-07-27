using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class AddressLine : ValueObject
{
    public string? Value { get; private set; }
    private AddressLine() { }
    private AddressLine(string? value) => Value = value;
    public static Result<AddressLine> Create(string? value)
    {
        var addressLineResult = Normalize(value);
        if (addressLineResult.IsFailure)
            return Result<AddressLine>.Fail(addressLineResult.Errors);

        return Result<AddressLine>.Ok(new AddressLine(addressLineResult.Data));
    }
    private static Result<string?> Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<string?>.Ok(null);

        value = Regex.Replace(value.Trim().ToLowerInvariant(), @"\s+", " ");

        if (value.Length > 100)
            return Result<string?>.Fail("Complemento deve ter no máximo 100 caracteres.");

        if (value.Distinct().Count() == 1)
            return Result<string?>.Fail("Complemento deve ter mais de um caracter.");

        return Result<string?>.Ok(value);
    }
    public override string? ToString() => Value;
    public static implicit operator string?(AddressLine addressLine) => addressLine.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}