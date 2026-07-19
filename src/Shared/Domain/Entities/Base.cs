namespace DemoMVC.Shared.Domain.Entities;

public class Base
{
    public Guid Id { get; protected set; }
    public bool IsActive { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }
    protected Base() { }
}