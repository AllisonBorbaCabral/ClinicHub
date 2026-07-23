using DemoMVC.Application.Patients.Interfaces;
using DemoMVC.Domain.Patients.ValueObjects;
using DemoMVC.Application.Interfaces;
using DemoMVC.Shared.Constants;

namespace DemoMVC.Infrastructure.Patients.Services;

public class MedicalRecordGenerator : IMedicalRecordGenerator
{
    private readonly ISequenceGenerator _sequence;
    public MedicalRecordGenerator(ISequenceGenerator sequence)
    {
        _sequence = sequence;
    }
    public async Task<MedicalRecord> NextAsync()
    {
        var value = await _sequence.NextAsync(DatabaseSequences.MedicalRecord);

        var result = MedicalRecord.Create((int)value);

        if (!result.Success)
            throw new InvalidOperationException("Erro na geração da sequência.");

        return result.Data!;
    }
}