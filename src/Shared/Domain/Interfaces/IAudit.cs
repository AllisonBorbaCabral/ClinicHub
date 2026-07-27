namespace DemoMVC.Shared.Domain.Interfaces;

public interface IAudit
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
    DateTime? DeletedAt { get; }
}