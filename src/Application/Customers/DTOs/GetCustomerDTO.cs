namespace DemoMVC.Application.Customers.DTOs;

public sealed record GetCustomerDTO(
    string Id,
    string Name,
    DateOnly BirthDate,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsActive
);