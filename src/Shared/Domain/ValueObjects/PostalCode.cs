using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class PostalCode : ValueObject
{
    public string Value { get; private set; } = null!;
    private PostalCode() { }
    private PostalCode(string value) => Value = value;
    public static Result<PostalCode> Create(string? value)
    {
        var postalCodeResult = Normalize(value);
        if (postalCodeResult.IsFailure)
            return Result<PostalCode>.Fail(postalCodeResult.Errors);

        return Result<PostalCode>.Ok(new PostalCode(postalCodeResult.Data));
    }
    private static Result<string> Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<string>.Fail("CEP não pode ser vazio ou nulo.");

        value = value.Trim();

        if (!Regex.IsMatch(value, @"^\d+$"))
            return Result<string>.Fail("CEP deve conter apenas números.");

        if (value.Length != 8)
            return Result<string>.Fail("CEP inválido.");

        if (value.Distinct().Count() == 1)
            return Result<string>.Fail("CEP deve ter mais de um caracter.");

        return Result<string>.Ok(value);
    }
    public override string ToString() => Value;
    public static implicit operator string(PostalCode postalCode) => postalCode.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}