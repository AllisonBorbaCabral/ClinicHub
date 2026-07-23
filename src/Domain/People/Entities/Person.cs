using DemoMVC.Shared.Results;
using DemoMVC.Shared.Domain.Entities;
using DemoMVC.Domain.People.ValueObjects;
using DemoMVC.Shared.Domain.ValueObjects;
using DemoMVC.Domain.Patients.Entities;

namespace DemoMVC.Domain.People.Entities;

public class Person : Base
{
    public PersonName Name { get; private set; } = null!;
    public BirthDate BirthDate { get; private set; } = null!;
    public Cpf Cpf { get; private set; } = null!;
    public Address? Address { get; private set; }
    public Contact? Contact { get; private set; }
    public Patient? Patient { get; private set; }
    private Person() { }
    private Person(
        PersonName name,
        BirthDate birthDate,
        Cpf cpf,
        Address? address,
        Contact? contact)
    {
        Id = Guid.NewGuid();
        Name = name;
        BirthDate = birthDate;
        Cpf = cpf;
        Address = address;
        Contact = contact;
        IsActive = true;
        IsDeleted = false;
        CreatedAt = DateTime.UtcNow;
    }
    public static Result<Person> Create(
        string name,
        DateOnly birthDate,
        string cpf,
        string? street,
        string? streetNumber,
        string? neighborhood,
        string? addressLine,
        string? postalCode,
        string? phoneNumber,
        string? email)
    {
        if (string.IsNullOrEmpty(name))
            return Result<Person>.Fail("Nome não pode ser nulo ou vazio.");

        var nameObj = PersonName.Create(name);
        if (!nameObj.Success)
            return Result<Person>.Fail("Ocorreu um erro ao criar Nome.");

        var birthDateObj = BirthDate.Create(birthDate);
        if (!birthDateObj.Success)
            return Result<Person>.Fail("Ocorreu um erro ao criar Data de Nascimento.");

        var cpfObj = Cpf.Create(cpf);
        if (!cpfObj.Success)
            return Result<Person>.Fail("Ocorreu um erro ao criar CPF.");

        var address = CreateAddress(
            street,
            streetNumber,
            neighborhood,
            addressLine,
            postalCode
        );

        var contact = CreateContact(
            phoneNumber,
            email
        );

        return Result<Person>.Ok(new Person(
                nameObj.Data!,
                birthDateObj.Data!,
                cpfObj.Data!,
                address.Data,
                contact.Data
            ));
    }
    private static Result<Address?> CreateAddress(
        string? street,
        string? streetNumber,
        string? neighborhood,
        string? addressLine,
        string? postalCode
    )
    {
        if (string.IsNullOrEmpty(street))
            return Result<Address?>.Ok(null);

        if (string.IsNullOrEmpty(streetNumber))
            return Result<Address?>.Ok(null);

        if (string.IsNullOrEmpty(neighborhood))
            return Result<Address?>.Ok(null);

        if (string.IsNullOrEmpty(postalCode))
            return Result<Address?>.Ok(null);

        var address = Address.Create(
            street,
            streetNumber,
            neighborhood,
            addressLine,
            postalCode
        );
        if (!address.Success)
            return Result<Address?>.Fail("Falha ao criar endereço");
        return Result<Address?>.Ok(address.Data!);
    }
    private static Result<Contact?> CreateContact(
        string? phoneNumber,
        string? email
    )
    {
        if (string.IsNullOrEmpty(phoneNumber))
            return Result<Contact?>.Ok(null);

        if (string.IsNullOrEmpty(email))
            return Result<Contact?>.Ok(null);

        var contact = Contact.Create(
            phoneNumber,
            email
        );
        if (!contact.Success)
            return Result<Contact?>.Fail("Falha ao criar contato");
        return Result<Contact?>.Ok(contact.Data!);
    }
}