namespace DemoMVC.Application.People.DTOs;

public sealed record ListPersonDTO(
    string Id,
    string Name,
    DateOnly BirthDate,
    string Cpf,
    bool IsActive
);