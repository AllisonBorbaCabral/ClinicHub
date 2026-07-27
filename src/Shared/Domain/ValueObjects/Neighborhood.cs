using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class Neighborhood : ValueObject
{
    public string Value { get; private set; } = null!;
    private Neighborhood() { }
    private Neighborhood(string value) => Value = value;
    public static Result<Neighborhood> Create(string? value)
    {
        var neighborhoodResult = Normalize(value);
        if (neighborhoodResult.IsFailure)
            return Result<Neighborhood>.Fail(neighborhoodResult.Errors);

        return Result<Neighborhood>.Ok(new Neighborhood(neighborhoodResult.Data));
    }
    private static Result<string> Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<string>.Fail("Bairro não pode ser vazio ou nulo.");

        value = Regex.Replace(value.Trim(), @"\s+", " ").ToLowerInvariant();

        if (value.Length < 3)
            return Result<string>.Fail("Bairro deve ter no mínimo 3 caracteres.");

        if (value.Length > 200)
            return Result<string>.Fail("Bairro deve ter no máximo 200 caracteres.");

        if (value.Distinct().Count() == 1)
            return Result<string>.Fail("Bairro deve ter mais de um caracter.");

        return Result<string>.Ok(value);
    }
    public override string ToString() => Value;
    public static implicit operator string(Neighborhood neighborhood) => neighborhood.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}