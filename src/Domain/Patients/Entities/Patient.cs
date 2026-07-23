using DemoMVC.Shared.Results;
using DemoMVC.Domain.Patients.Enums;
using DemoMVC.Shared.Domain.Entities;
using DemoMVC.Domain.People.Entities;
using DemoMVC.Domain.Patients.ValueObjects;

namespace DemoMVC.Domain.Patients.Entities;

public class Patient : Base
{
    public MedicalRecord MedicalRecord { get; private set; } = null!;
    public Guid PersonId { get; private set; }
    public Person Person { get; private set; } = null!;
    public InsurancePlan? Insurance { get; private set; }
    public BloodType BloodType { get; private set; }
    public Responsible? Responsible { get; private set; }
    public EmergencyContact? EmergencyContact { get; private set; }

    private Patient() { } //EF Core
    private Patient(
        MedicalRecord medicalRecord,
        Person person,
        InsurancePlan? insurance,
        BloodType bloodType,
        Responsible? responsible,
        EmergencyContact? emergencyContact)
    {
        Id = Guid.NewGuid();
        MedicalRecord = medicalRecord;
        PersonId = person.Id;
        Person = person;
        Insurance = insurance;
        BloodType = bloodType;
        Responsible = responsible;
        EmergencyContact = emergencyContact;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
        IsDeleted = false;
    }
    public static Result<Patient> Create(
        int medicalRecord,
        Person person,
        string? insurancePlanId,
        DateOnly? dueDate,
        int? bloodType,
        string? nameResponsible,
        string? cpfResponsible,
        int? kinshipResponsible,
        string? numberResponsible,
        string? nameEmergency,
        string? numberEmergency,
        int? kinshipEmergency
        )
    {
        var medical = CreateMedicalRecord(medicalRecord);

        if (person == null)
            return Result<Patient>.Fail("Vínculo com pessoa não pode ser nulo.");

        var insurance = CreateInsurance(insurancePlanId, dueDate);

        var responsible = CreateResponsible(nameResponsible, cpfResponsible, kinshipResponsible, numberResponsible);

        if (!bloodType.HasValue)
            return Result<Patient>.Fail("Tipo sanguíneo é obrigatório.");

        var blood = (BloodType)bloodType;

        var emergency = CreateEmergency(nameEmergency, numberEmergency, kinshipEmergency);

        return Result<Patient>.Ok(new Patient(
            medical.Data!,
            person,
            insurance.Data,
            blood,
            responsible.Data,
            emergency.Data
        ));
    }
    private static Result<MedicalRecord> CreateMedicalRecord(
        int medicalRecord
    )
    {
        var medical = MedicalRecord.Create(medicalRecord);

        if (!medical.Success)
            return Result<MedicalRecord>.Fail("Não foi possível criar o Prontuário.");

        return Result<MedicalRecord>.Ok(medical.Data!);
    }
    private static Result<InsurancePlan?> CreateInsurance(
        string? insuranceId,
        DateOnly? dueDate
    )
    {
        if (string.IsNullOrEmpty(insuranceId))
            return Result<InsurancePlan?>.Fail("O código do plano não pode ser nulo.");

        if (dueDate == null)
            return Result<InsurancePlan?>.Fail("O vencimento não pode ser nulo.");

        var insurancePlan = InsurancePlan.Create(insuranceId, dueDate.Value);

        if (!insurancePlan.Success)
            return Result<InsurancePlan?>.Fail("Não foi possível criar o plano.");

        return Result<InsurancePlan?>.Ok(insurancePlan.Data);
    }
    private static Result<Responsible?> CreateResponsible(
        string? name,
        string? cpf,
        int? kinship,
        string? number
    )
    {
        if (string.IsNullOrEmpty(name))
            return Result<Responsible?>.Fail("Nome do responsável não pode ser nulo.");

        if (string.IsNullOrEmpty(cpf))
            return Result<Responsible?>.Fail("CPF do responsável não pode ser nulo.");

        if (!kinship.HasValue)
            return Result<Responsible?>.Fail("Parentesco do responsável não pode ser nulo.");

        if (string.IsNullOrEmpty(number))
            return Result<Responsible?>.Fail("Número do responsável não pode ser nulo.");

        var responsible = Responsible.Create(
                name,
                cpf,
                kinship.Value,
                number);

        if (!responsible.Success)
            return Result<Responsible?>.Fail("Não foi possível criar o responsável.");

        return Result<Responsible?>.Ok(responsible.Data);
    }
    private static Result<EmergencyContact?> CreateEmergency(
        string? name,
        string? number,
        int? kinship
    )
    {
        if (string.IsNullOrEmpty(name))
            return Result<EmergencyContact?>.Fail("Nome do contato de emergência não pode ser nulo.");

        if (string.IsNullOrEmpty(number))
            return Result<EmergencyContact?>.Fail("Numéro do contato de emergência não pode ser nulo.");

        if (!kinship.HasValue)
            return Result<EmergencyContact?>.Fail("Parentesco do contato de emergência não pode ser nulo.");

        var emergency = EmergencyContact.Create(
                name,
                number,
                kinship.Value);

        if (!emergency.Success)
            return Result<EmergencyContact?>.Fail("Não foi possível criar o contato de emergência.");

        return Result<EmergencyContact?>.Ok(emergency.Data);
    }
}