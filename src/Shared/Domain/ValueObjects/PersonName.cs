using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class PersonName : ValueObject
{
    public string Value { get; private set; } = null!;
    private PersonName() { }
    private PersonName(string value) => Value = value;
    public static Result<PersonName> Create(string? value)
    {
        var personNameResult = Normalize(value);
        if (personNameResult.IsFailure)
            return Result<PersonName>.Fail(personNameResult.Errors);

        return Result<PersonName>.Ok(new PersonName(personNameResult.Data));
    }
    private static Result<string> Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<string>.Fail("Nome não pode ser nulo ou vazio.");

        value = Regex.Replace(value.Trim(), @"\s+", " ").ToLowerInvariant();

        if (value.Length < 5)
            return Result<string>.Fail("Nome deve ter no mínimo 5 caracteres.");

        if (value.Length > 200)
            return Result<string>.Fail("Nome deve ter no máximo 200 caracteres.");

        if (value.Distinct().Count() == 1)
            return Result<string>.Fail("nome deve ter mais de um caracter.");

        return Result<string>.Ok(value);
    }
    public override string ToString() => Value;
    public static implicit operator string(PersonName personName) => personName.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}