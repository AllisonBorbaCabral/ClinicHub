using DemoMVC.Shared.Results;
using DemoMVC.Shared.Domain.ValueObjects;

namespace DemoMVC.Domain.People.ValueObjects;

public sealed class Contact
{
    public PhoneNumber? Phone { get; private set; }
    public Email? Email { get; private set; }
    private Contact() { }
    private Contact(PhoneNumber phone, Email email)
    {
        Phone = phone;
        Email = email;
    }
    public static Result<Contact> Create(string phone, string email)
    {
        var phoneObj = PhoneNumber.Create(phone);
        if (!phoneObj.Success || phoneObj.Data == null)
            return Result<Contact>.Fail("Erro ao criar número.");

        var emailObj = Email.Create(email);
        if (!emailObj.Success || emailObj.Data == null)
            return Result<Contact>.Fail("Erro ao criar email.");
        return Result<Contact>.Ok(new Contact(
            phoneObj.Data,
            emailObj.Data
        ));
    }
}