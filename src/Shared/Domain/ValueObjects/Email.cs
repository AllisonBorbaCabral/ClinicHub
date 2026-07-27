using System.Net.Mail;
using DemoMVC.Shared.Results;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string? Value { get; private set; }
    private Email() { }
    private Email(string? value) => Value = value;
    public static Result<Email> Create(string? value)
    {
        var emailResult = Normalize(value);
        if (emailResult.IsFailure)
            return Result<Email>.Fail(emailResult.Errors);

        return Result<Email>.Ok(new Email(emailResult.Data));
    }
    private static Result<string?> Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<string?>.Ok(null);

        value = value.Trim().ToLowerInvariant();

        if (value.Length > 254)
            return Result<string?>.Fail("E-mail não pode conter mais que 254 caracteres.");

        if (value.Contains(' '))
            return Result<string?>.Fail("E-mail não pode conter espaços em branco.");

        if (!IsValid(value))
            return Result<string?>.Fail("E-mail inválido.");

        return Result<string?>.Ok(value);
    }
    private static bool IsValid(string value)
    {
        var email = new MailAddress(value);

        if (email.Address != value)
            return false;
        return true;
    }
    public override string? ToString() => Value;
    public static implicit operator string?(Email email) => email.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}