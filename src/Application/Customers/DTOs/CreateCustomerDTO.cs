namespace DemoMVC.Application.Customers.DTOs;

public sealed record CreateCustomerDTO(
    string Name,
    DateOnly BirthDate,
    bool IsActive
);