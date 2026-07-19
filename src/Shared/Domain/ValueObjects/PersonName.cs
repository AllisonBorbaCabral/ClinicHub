using DemoMVC.Shared.Results;
using System.Text.RegularExpressions;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class PersonName
{
    public string Value { get; private set; } = null!;
    private PersonName() { }
    private PersonName(string value)
    {
        Value = value;
    }
    public static Result<PersonName> Create(string value)
    {
        var personName = value.Trim();

        personName = Regex.Replace(personName, @"\s+", " ");

        if (string.IsNullOrWhiteSpace(personName))
            return Result<PersonName>.Fail("O campo nome não pode ser vazio.");

        if (personName.Length < 5)
            return Result<PersonName>.Fail("O campo nome deve ter no mínimo 5 caracteres.");

        if (personName.Length > 200)
            return Result<PersonName>.Fail("O campo nome deve ter no máximo 200 caracteres.");

        if (personName.Distinct().Count() == 1)
            return Result<PersonName>.Fail("O campo nome deve ter mais de um caracter.");

        return Result<PersonName>.Ok(new PersonName(personName));
    }
}