namespace DemoMVC.Domain.Customers.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    private Customer() { } //EF Core
    private Customer(string name, DateOnly birthDate, bool isActive)
    {
        Id = Guid.NewGuid();
        Name = name;
        BirthDate = birthDate;
        CreatedAt = DateTime.UtcNow;
        IsActive = isActive;
    }
    public static Customer Create(string name, DateOnly birthDate, bool isActive)
    {
        var obj = new Customer(name, birthDate, isActive);


        // ajustar retorno para quando implementar result
        return obj;
    }
}