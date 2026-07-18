using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Domain.People.ValueObjects;

public sealed class Neighborhood
{
    public string Value { get; private set; } = null!;
    private Neighborhood() { }
    private Neighborhood(string value)
    {
        Value = value;
    }
    public static Result<Neighborhood> Create(string value)
    {
        var neighborhood = value.Trim();

        neighborhood = Regex.Replace(neighborhood, @"\s+", " ");

        if (string.IsNullOrWhiteSpace(neighborhood))
            return Result<Neighborhood>.Fail("O campo bairro não pode ser vazio.");

        if (neighborhood.Length < 3)
            return Result<Neighborhood>.Fail("O campo bairro deve ter no mínimo 3 caracteres.");

        if (neighborhood.Length > 200)
            return Result<Neighborhood>.Fail("O campo bairro deve ter no máximo 200 caracteres.");

        if (neighborhood.Distinct().Count() == 1)
            return Result<Neighborhood>.Fail("O campo bairro deve ter mais de um caracter.");

        return Result<Neighborhood>.Ok(new Neighborhood(neighborhood));
    }
}