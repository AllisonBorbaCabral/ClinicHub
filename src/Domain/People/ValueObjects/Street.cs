using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Domain.People.ValueObjects;

public sealed class Street
{
    public string Value { get; private set; } = null!;
    private Street() { }
    private Street(string value)
    {
        Value = value;
    }
    public Result<Street> Create(string value)
    {
        var street = value.Trim();

        street = Regex.Replace(street, @"\s+", " ");

        if (string.IsNullOrWhiteSpace(street))
            return Result<Street>.Fail("O campo rua não pode ser vazio.");

        if (street.Length < 3)
            return Result<Street>.Fail("O campo rua deve ter no mínimo 3 caracteres.");

        if (street.Length > 200)
            return Result<Street>.Fail("O campo rua deve ter no máximo 200 caracteres.");

        if (street.Distinct().Count() == 1)
            return Result<Street>.Fail("O campo rua deve ter mais de um caracter.");

        return Result<Street>.Ok(new Street(street));
    }
}