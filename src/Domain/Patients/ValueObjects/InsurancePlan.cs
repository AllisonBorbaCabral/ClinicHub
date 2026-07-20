using DemoMVC.Shared.Results;

namespace DemoMVC.Domain.Patients.ValueObjects;

public sealed class InsurancePlan
{
    public string Value { get; private set; } = null!;
    public DateOnly DueDate { get; private set; }
    private InsurancePlan() { }
    private InsurancePlan(string value, DateOnly dueDate)
    {
        Value = value;
        DueDate = dueDate;
    }
    public static Result<InsurancePlan> Create(string value, DateOnly dueDate)
    {
        if (string.IsNullOrEmpty(value))
            return Result<InsurancePlan>.Fail("A Carteira não pode ser vazia.");

        return Result<InsurancePlan>.Ok(new InsurancePlan(value, dueDate));
    }
}