using DemoMVC.Shared.Domain.Interfaces;

namespace DemoMVC.Shared.Domain.Entities;

public abstract class Audit : IAudit
{
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }
    public void MarkAsUpdated() => UpdatedAt = DateTime.UtcNow;
    public void MarkAsDeleted() => DeletedAt = DateTime.UtcNow;
}