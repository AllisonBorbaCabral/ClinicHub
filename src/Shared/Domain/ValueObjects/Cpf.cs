using DemoMVC.Shared.Results;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class Cpf : ValueObject
{
    public string Value { get; private set; } = null!;
    private Cpf() { }
    private Cpf(string value) => Value = value;
    public static Result<Cpf> Create(string? value)
    {
        var cpfResult = Normalize(value);
        if (cpfResult.IsFailure)
            return Result<Cpf>.Fail(cpfResult.Errors);

        return Result<Cpf>.Ok(new Cpf(cpfResult.Data));
    }
    private static Result<string> Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<string>.Fail("CPF não pode ser nulo ou vazio.");

        value = new string(value.Trim().Where(char.IsDigit).ToArray());

        if (value.Length != 11)
            return Result<string>.Fail("CPF deve conter 11 dígitos.");

        if (value.Distinct().Count() == 1)
            return Result<string>.Fail("CPF deve conter mais de um caracter.");

        if (!IsValid(value))
            return Result<string>.Fail("CPF inválido.");

        return Result<string>.Ok(value);
    }
    private static bool IsValid(string value)
    {
        var firstDigit = CalculateDigit(value, 9, 10);
        var secondDigit = CalculateDigit(value, 10, 11);

        return value[9] == (char)(firstDigit + '0')
            && value[10] == (char)(secondDigit + '0');
    }
    private static int CalculateDigit(string value, int length, int initialWeight)
    {
        var sum = 0;
        var weight = initialWeight;

        for (var i = 0; i < length; i++)
        {
            var number = value[i] - '0';

            sum += number * weight;
            weight--;
        }

        var remainder = sum % 11;

        return remainder < 2 ? 0 : 11 - remainder;
    }
    private string Format()
    {
        if (string.IsNullOrWhiteSpace(Value) || Value.Length != 11)
            return Value ?? string.Empty;

        return $"{Value[..3]}.{Value[3..6]}.{Value[6..9]}-{Value[9..]}";
    }
    public override string ToString() => Format();
    public static implicit operator string(Cpf cpf) => cpf.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}