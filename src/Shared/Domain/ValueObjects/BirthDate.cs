using DemoMVC.Shared.Results;

namespace DemoMVC.Shared.Domain.ValueObjects;

public sealed class BirthDate : ValueObject
{
    public DateOnly Value { get; private set; }
    private BirthDate() { }
    private BirthDate(DateOnly value) => Value = value;
    public static Result<BirthDate> Create(DateOnly value)
    {
        var birthDateResult = Normalize(value);
        if (birthDateResult.IsFailure)
            return Result<BirthDate>.Fail(birthDateResult.Errors);
        return Result<BirthDate>.Ok(new BirthDate(value));
    }
    private static Result<DateOnly> Normalize(DateOnly value)
    {
        if (!IsValid(value))
            return Result<DateOnly>.Fail("Data de Nascimento inválida.");
        return Result<DateOnly>.Ok(value);
    }
    private static bool IsValid(DateOnly value)
    {
        DateOnly min = new DateOnly(1900, 1, 1);
        DateOnly max = DateOnly.FromDateTime(DateTime.Today);

        if (value < min || value > max)
            return false;
        return true;
    }
    public override string ToString() => Value.ToString();
    public static implicit operator DateOnly(BirthDate birthDate) => birthDate.Value;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}