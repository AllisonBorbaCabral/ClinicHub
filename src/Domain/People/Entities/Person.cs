using DemoMVC.Shared.Results;
using DemoMVC.Shared.Domain.Entities;
using DemoMVC.Domain.People.ValueObjects;
using DemoMVC.Shared.Domain.ValueObjects;

namespace DemoMVC.Domain.People.Entities;

public class Person : Base
{
    public PersonName Name { get; private set; } = null!;
    public BirthDate BirthDate { get; private set; } = null!;
    public Cpf Cpf { get; private set; } = null!;
    public Guid? AddressId { get; private set; }
    public Address? Address { get; private set; }
    public Guid? ContactId { get; private set; }
    public Contact? Contact { get; private set; }
    private Person() { }
    private Person(
        PersonName name,
        BirthDate birthDate,
        Cpf cpf,
        Address address,
        Contact contact)
    {
        Id = Guid.NewGuid();
        Name = name;
        BirthDate = birthDate;
        Cpf = cpf;
        AddressId = address.Id;
        Address = address;
        ContactId = contact.Id;
        Contact = contact;
        IsActive = true;
        IsDeleted = false;
        CreatedAt = DateTime.UtcNow;
    }
    public static Result<Person> Create(
        string name,
        DateOnly birthDate,
        string cpf,
        string street,
        string streetNumber,
        string neighborhood,
        string addressLine,
        string postalCode,
        string phoneNumber,
        string email)
    {
        if (string.IsNullOrEmpty(name))
            return Result<Person>.Fail("Nome não pode ser nulo ou vazio.");

        var nameObj = PersonName.Create(name);
        if (!nameObj.Success || nameObj.Data == null)
            return Result<Person>.Fail("Ocorreu um erro ao criar Nome.");

        var birthDateObj = BirthDate.Create(birthDate);
        if (!birthDateObj.Success || birthDateObj.Data == null)
            return Result<Person>.Fail("Ocorreu um erro ao criar Data de Nascimento.");

        var cpfObj = Cpf.Create(cpf);
        if (!cpfObj.Success || cpfObj.Data == null)
            return Result<Person>.Fail("Ocorreu um erro ao criar CPF.");

        var addressObj = Address.Create(
            street,
            streetNumber,
            neighborhood,
            addressLine,
            postalCode);
        if (!addressObj.Success || addressObj.Data == null)
            return Result<Person>.Fail("Ocorreu um erro ao criar Endereço.");

        var contactObj = Contact.Create(phoneNumber, email);
        if (!contactObj.Success || contactObj.Data == null)
            return Result<Person>.Fail("Ocorreu um erro ao criar Contato.");

        return Result<Person>.Ok(new Person(
            nameObj.Data,
            birthDateObj.Data,
            cpfObj.Data,
            addressObj.Data,
            contactObj.Data
        ));
    }
}