using DemoMVC.Shared.Results;

namespace DemoMVC.Domain.People.ValueObjects;

public sealed class BirthDate
{
    public DateOnly Value { get; private set; }
    private BirthDate() { }
    private BirthDate(DateOnly value)
    {
        Value = value;
    }
    public static Result<BirthDate> Create(DateOnly value)
    {
        DateOnly min = new DateOnly(1900, 1, 1);
        DateOnly max = DateOnly.FromDateTime(DateTime.Today);

        if (value < min || value > max)
            return Result<BirthDate>.Fail("Data de nascimento inválida.");

        return Result<BirthDate>.Ok(new BirthDate(value));
    }
}