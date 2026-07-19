using DemoMVC.Shared.Results;
using DemoMVC.Shared.Domain.Entities;
using DemoMVC.Shared.Domain.ValueObjects;

namespace DemoMVC.Domain.People.Entities;

public class Contact : Base
{
    public PhoneNumber? Phone { get; private set; }
    public Email? Email { get; private set; }
    private Contact() { }
    private Contact(PhoneNumber phone, Email email)
    {
        Id = Guid.NewGuid();
        Phone = phone;
        Email = email;
        IsActive = true;
        IsDeleted = false;
        CreatedAt = DateTime.UtcNow;
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