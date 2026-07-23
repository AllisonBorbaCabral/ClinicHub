using DemoMVC.Shared.Results;

namespace DemoMVC.Domain.Patients.ValueObjects;

public sealed class MedicalRecord
{
    public int Value { get; private set; }
    private MedicalRecord() { }
    private MedicalRecord(int value)
    {
        Value = value;
    }
    public static Result<MedicalRecord> Create(int medicalRecord)
    {
        return Result<MedicalRecord>.Ok(new MedicalRecord(medicalRecord));
    }
}