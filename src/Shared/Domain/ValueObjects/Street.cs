using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class Street : ValueObject
{
    public string Value { get; private set; } = null!;
    private Street() { }
    private Street(string value)
    {
        Value = value;
    }
    public static Result<Street> Create(string? value)
    {
        var streetResult = Normalize(value);
        if (streetResult.IsFailure)
            return Result<Street>.Fail(streetResult.Errors);

        return Result<Street>.Ok(new Street(streetResult.Data));
    }
    private static Result<string> Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<string>.Fail("Rua não pode ser vazio ou nulo.");

        value = Regex.Replace(value.Trim(), @"\s+", " ");

        if (value.Length < 3)
            return Result<string>.Fail("Rua deve ter no mínimo 3 caracteres.");

        if (value.Length > 200)
            return Result<string>.Fail("Rua deve ter no máximo 200 caracteres.");

        if (value.Distinct().Count() == 1)
            return Result<string>.Fail("Rua deve ter mais de um caracter.");

        return Result<string>.Ok(value);
    }
    public override string ToString() => Value;
    public static implicit operator string(Street street) => street.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}