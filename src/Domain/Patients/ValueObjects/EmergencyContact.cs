using DemoMVC.Shared.Results;
using DemoMVC.Domain.Patients.Enums;
using DemoMVC.Shared.Domain.ValueObjects;

namespace DemoMVC.Domain.Patients.ValueObjects;

public sealed class EmergencyContact
{
    public PersonName Name { get; private set; } = null!;
    public PhoneNumber Number { get; private set; } = null!;
    public Kinship Kinship { get; private set; }
    private EmergencyContact() { }
    private EmergencyContact(PersonName name, PhoneNumber value, Kinship kinship)
    {
        Name = name;
        Number = value;
        Kinship = kinship;
    }
    public static Result<EmergencyContact> Create(string name, string number, int kinship)
    {
        if (string.IsNullOrEmpty(name))
            return Result<EmergencyContact>.Fail("O nome não pode ser vazio ou nulo.");

        if (string.IsNullOrEmpty(number))
            return Result<EmergencyContact>.Fail("O número não pode ser vazio ou nulo.");

        if (kinship == 0)
            return Result<EmergencyContact>.Fail("O tipo de parentesco é inválido.");

        var nameObj = PersonName.Create(name);
        if (!nameObj.Success || nameObj.Data == null)
            return Result<EmergencyContact>.Fail("Falha ao criar o nome.");

        var numberObj = PhoneNumber.Create(number);
        if (!numberObj.Success || numberObj.Data == null)
            return Result<EmergencyContact>.Fail("Falha ao criar o número");

        var kinshipObj = (Kinship)kinship;

        return Result<EmergencyContact>.Ok(new EmergencyContact(
            nameObj.Data,
            numberObj.Data,
            kinshipObj));
    }
}