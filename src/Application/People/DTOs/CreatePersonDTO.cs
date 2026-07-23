namespace DemoMVC.Application.People.DTOs;

public sealed record CreatePersonDTO(
    string Name,
    DateOnly BirthDate,
    string Cpf,
    string Street,
    string StreetNumber,
    string Neighborhood,
    string AddressLine,
    string PostalCode,
    string PhoneNumber,
    string Email,
    bool IsActive
);