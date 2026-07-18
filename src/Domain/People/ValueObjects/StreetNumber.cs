using System.Text.RegularExpressions;
using DemoMVC.Shared.Results;

namespace DemoMVC.Domain.People.ValueObjects;

public sealed class StreetNumber
{
    public string Value { get; private set; } = null!;
    private StreetNumber() { }
    private StreetNumber(string value)
    {
        Value = value;
    }
    public Result<StreetNumber> Create(string value)
    {
        var streetNumber = value.Trim();

        if (!Regex.IsMatch(streetNumber, @"^\d+$"))
            return Result<StreetNumber>.Fail("O número do endereço deve conter apenas dígitos numéricos.");

        if (streetNumber.Length > 10)
            return Result<StreetNumber>.Fail("O número do endereço deve conter no máximo 10 caracteres.");

        return Result<StreetNumber>.Ok(new StreetNumber(streetNumber));
    }
}