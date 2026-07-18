using System.Text.RegularExpressions;
using DemoMVC.Shared.Results;

namespace DemoMVC.Domain.People.ValueObjects;

public sealed class AddressLine
{
    public string Value { get; private set; } = null!;
    private AddressLine() { }
    private AddressLine(string value)
    {
        Value = value;
    }
    public static Result<AddressLine> Create(string value)
    {
        var addressLine = value.Trim();

        addressLine = Regex.Replace(addressLine, @"\s+", " ");

        if (string.IsNullOrWhiteSpace(addressLine))
            return Result<AddressLine>.Fail("Complemento não pode ser nulo ou vazio.");

        if (addressLine.Length > 100)
            return Result<AddressLine>.Fail("Complemento deve ter no máximo 100 caracteres.");

        if (addressLine.Distinct().Count() == 1)
            return Result<AddressLine>.Fail("Complemento deve ter mais de um caracter.");

        return Result<AddressLine>.Ok(new AddressLine(addressLine));
    }
}