using System.Text.RegularExpressions;
using DemoMVC.Shared.Results;

namespace DemoMVC.Domain.People.ValueObjects;

public sealed class PostalCode
{
    public string Value { get; private set; } = null!;
    private PostalCode() { }
    private PostalCode(string value)
    {
        Value = value;
    }
    public Result<PostalCode> Create(string value)
    {
        var postalCode = value.Trim();

        if (string.IsNullOrWhiteSpace(postalCode))
            return Result<PostalCode>.Fail("CEP não pode ser nulo ou vazio.");

        if (!Regex.IsMatch(postalCode, @"^\d+$"))
            return Result<PostalCode>.Fail("CEP deve conter apenas caracteres númericos.");

        if (postalCode.Length > 8 || postalCode.Length < 8)
            return Result<PostalCode>.Fail("CEP inválido.");

        if (postalCode.Distinct().Count() == 1)
            return Result<PostalCode>.Fail("CEP deve ter mais de um caracter.");

        return Result<PostalCode>.Ok(new PostalCode(postalCode));
    }
}