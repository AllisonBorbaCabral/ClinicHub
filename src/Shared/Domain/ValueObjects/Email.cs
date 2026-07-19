using DemoMVC.Shared.Results;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; private set; } = null!;
    private Email() { } // EF Core
    private Email(string value)
    {
        Value = value;
    }
    public static Result<Email> Create(string value)
    {
        value = value.Trim();

        if (string.IsNullOrWhiteSpace(value))
            return Result<Email>.Fail("O campo email não pode ser vazio.");

        if (value.Length > 255)
            return Result<Email>.Fail("O campo email não pode ser maior que 255 caracteres.");

        if (value.Contains(' '))
            return Result<Email>.Fail("O campo email não pode ter espaços em branco.");

        return Result<Email>.Ok(new Email(value));
    }
    public override string ToString()
    {
        return Value;
    }
}