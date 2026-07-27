using DemoMVC.Shared.Domain.Interfaces;

namespace DemoMVC.Shared.Domain.Entities;

public abstract class Entity : Audit, IEntity
{
    public Guid Id { get; protected set; } = Guid.CreateVersion7();
    public bool IsActive { get; protected set; } = true;
    public bool IsDeleted => DeletedAt.HasValue;
    protected Entity() { }
    protected Entity(Guid id) => Id = id;
    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
    public void Delete()
    {
        MarkAsDeleted();
        IsActive = false;
    }
}