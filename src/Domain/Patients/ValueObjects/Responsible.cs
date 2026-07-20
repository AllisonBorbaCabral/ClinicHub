using DemoMVC.Domain.Patients.Enums;
using DemoMVC.Shared.Domain.ValueObjects;
using DemoMVC.Shared.Results;

namespace DemoMVC.Domain.Patients.ValueObjects;

public sealed class Responsible
{
    public PersonName Name { get; private set; } = null!;
    public Cpf Cpf { get; private set; } = null!;
    public Kinship Kinship { get; private set; }
    public PhoneNumber Number { get; private set; } = null!;
    private Responsible() { }
    private Responsible(
        PersonName name,
        Cpf cpf,
        Kinship kinship,
        PhoneNumber number)
    {
        Name = name;
        Cpf = cpf;
        Kinship = kinship;
        Number = number;
    }
    public static Result<Responsible> Create(
        string name,
        string cpf,
        int kinship,
        string number)
    {
        if (string.IsNullOrEmpty(name))
            return Result<Responsible>.Fail("Nome é obrigatório.");
        if (string.IsNullOrEmpty(cpf))
            return Result<Responsible>.Fail("CPF é obrigatório.");
        if (kinship == 0)
            return Result<Responsible>.Fail("Parentesco inválido.");
        if (string.IsNullOrEmpty(number))
            return Result<Responsible>.Fail("Contato é obrigatório.");

        var nameObj = PersonName.Create(name);
        if (!nameObj.Success || nameObj.Data == null)
            return Result<Responsible>.Fail("Falha ao criar Nome.");

        var cpfObj = Cpf.Create(cpf);
        if (!cpfObj.Success || cpfObj.Data == null)
            return Result<Responsible>.Fail("Falha ao criar CPF.");

        var kinshipEnum = (Kinship)kinship;

        var numberObj = PhoneNumber.Create(number);
        if (!numberObj.Success || numberObj.Data == null)
            return Result<Responsible>.Fail("Falha ao criar Número.");

        return Result<Responsible>.Ok(new Responsible(
            nameObj.Data,
            cpfObj.Data,
            kinshipEnum,
            numberObj.Data
        ));
    }
}