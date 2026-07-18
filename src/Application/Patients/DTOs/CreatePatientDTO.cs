namespace DemoMVC.Application.Patients.DTOs;

public sealed record CreatePatientDTO(
    string Name,
    DateOnly BirthDate,
    bool IsActive
);