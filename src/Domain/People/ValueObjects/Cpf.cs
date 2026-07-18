using DemoMVC.Shared.Results;

namespace DemoMVC.Domain.People.ValueObjects;

public sealed class Cpf
{
    public string Value { get; private set; } = null!;
    private Cpf() { } // EF Core
    private Cpf(string value)
    {
        Value = value;
    }
    public static Result<Cpf> Create(string value)
    {
        var normalized = Normalize(value);

        if (!IsValid(normalized))
            return Result<Cpf>.Fail("CPF inválido.");

        return Result<Cpf>.Ok(new Cpf(normalized));
    }
    private static bool IsValid(string cPF)
    {
        if (string.IsNullOrWhiteSpace(cPF))
            return false;

        var cpf = Normalize(cPF);

        if (cpf.Length != 11)
            return false;

        if (cpf.Distinct().Count() == 1)
            return false;

        var firstDigit = CalculateDigit(cpf, 9, 10);
        var secondDigit = CalculateDigit(cpf, 10, 11);

        return cpf[9] == firstDigit.ToString()[0] &&
               cpf[10] == secondDigit.ToString()[0];
    }
    private static string Normalize(string value)
    {
        return new string(value.Where(char.IsDigit).ToArray());
    }
    private static int CalculateDigit(string cPF, int length, int initialWeight)
    {
        var sum = 0;
        var weight = initialWeight;

        for (var i = 0; i < length; i++)
        {
            var number = cPF[i] - '0';

            sum += number * weight;
            weight--;
        }

        var remainder = sum % 11;

        return remainder < 2 ? 0 : 11 - remainder;
    }
    public override string ToString()
    {
        return Value;
    }
}