using DemoMVC.Shared.Results;
using DemoMVC.Shared.Domain.Entities;
using DemoMVC.Domain.People.Entities;
using DemoMVC.Domain.Patients.Enums;
using DemoMVC.Domain.Patients.ValueObjects;

namespace DemoMVC.Domain.Patients.Entities;

public class Patient : Base
{
    public int MedicalRecord { get; private set; }
    public Person Person { get; private set; } = null!;
    public InsurancePlan? Insurance { get; private set; }
    public BloodType BloodType { get; private set; }
    public Responsible? Responsible { get; private set; }
    public EmergencyContact? EmergencyContact { get; private set; }

    private Patient() { } //EF Core
    private Patient(
        Person person,
        InsurancePlan? insurance,
        BloodType bloodType,
        Responsible? responsible,
        EmergencyContact? emergencyContact)
    {
        Id = Guid.NewGuid();
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
        Person person,
        string? insurancePlanId,
        DateOnly dueDate,
        int bloodType,
        string? nameResponsible,
        string? cpfResponsible,
        int kinshipResponsible,
        string? numberResponsible,
        string? nameEmergency,
        string? numberEmergency,
        int kinshipEmergency
        )
    {
        Result<InsurancePlan> insurance = Result<InsurancePlan>.Fail();
        Result<Responsible> responsible = Result<Responsible>.Fail();
        Result<EmergencyContact> emergencyContact = Result<EmergencyContact>.Fail();

        if (person == null)
            return Result<Patient>.Fail("Vínculo com pessoa não pode ser nulo.");

        if (!string.IsNullOrEmpty(insurancePlanId))
        {
            insurance = InsurancePlan.Create(insurancePlanId, dueDate);
            if (!insurance.Success || insurance.Data == null)
                return Result<Patient>.Fail("Falha ao criar o Plano.");
        }

        if (bloodType == 0)
            return Result<Patient>.Fail("Tipo sanguíneo inválido.");

        var blood = (BloodType)bloodType;

        if (!string.IsNullOrEmpty(nameResponsible))
        {
            if (string.IsNullOrEmpty(cpfResponsible))
                return Result<Patient>.Fail("CPF do responsável é obrigatório.");

            if (kinshipResponsible == 0)
                return Result<Patient>.Fail("Tipo de parentesco do responsável inválido.");

            if (string.IsNullOrEmpty(numberResponsible))
                return Result<Patient>.Fail("Número do responsável é obrigatório.");

            responsible = Responsible.Create(
                nameResponsible,
                cpfResponsible,
                kinshipResponsible,
                numberResponsible);
        }

        if (!string.IsNullOrEmpty(nameEmergency))
        {
            if (string.IsNullOrEmpty(numberEmergency))
                return Result<Patient>.Fail("Número do contato de emergência é obrigatório.");

            if (kinshipEmergency == 0)
                return Result<Patient>.Fail("Tipo de parentesco do contato de emergência inválido.");

            emergencyContact = EmergencyContact.Create(
                nameEmergency,
                numberEmergency,
                kinshipEmergency
            );
        }

        return Result<Patient>.Ok(new Patient(
            person,
            insurance.Success ? insurance.Data : null,
            blood,
            responsible.Success ? responsible.Data : null,
            emergencyContact.Success ? emergencyContact.Data : null
        ));
    }
}