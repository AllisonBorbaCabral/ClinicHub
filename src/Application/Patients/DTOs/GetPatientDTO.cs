namespace DemoMVC.Application.Patients.DTOs;

public sealed record GetPatientDTO(
    string Id,
    string Name,
    DateOnly BirthDate,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    bool IsActive
);