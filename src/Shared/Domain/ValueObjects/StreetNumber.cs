using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class StreetNumber : ValueObject
{
    public string Value { get; private set; } = null!;
    private StreetNumber() { }
    private StreetNumber(string value)
    {
        Value = value;
    }
    public static Result<StreetNumber> Create(string? value)
    {
        var streetNumberResult = Normalize(value);
        if (streetNumberResult.IsFailure)
            return Result<StreetNumber>.Fail(streetNumberResult.Errors);

        return Result<StreetNumber>.Ok(new StreetNumber(streetNumberResult.Data));
    }
    private static Result<string> Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<string>.Fail("Número do endereço não pode ser vazio ou nulo.");

        value = value.Trim();

        if (!Regex.IsMatch(value, @"^\d+$"))
            return Result<string>.Fail("Número do endereço deve conter apenas dígitos numéricos.");

        if (value.Length > 10)
            return Result<string>.Fail("Número do endereço deve conter no máximo 10 caracteres.");

        return Result<string>.Ok(value);
    }
    public override string ToString() => Value;
    public static implicit operator string(StreetNumber streetNumber) => streetNumber.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}