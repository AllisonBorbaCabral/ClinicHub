namespace DemoMVC.Application.Patients.DTOs;

public sealed record CreatePatientDTO(
    Guid? PersonId,
    string Name,
    DateOnly BirthDate,
    string Cpf,
    string? Street,
    string? StreetNumber,
    string? Neighborhood,
    string? AddressLine,
    string? PostalCode,
    string? PhoneNumber,
    string? Email,
    string? InsuranceId,
    DateOnly? InsuranceDueDate,
    int BloodType,
    string? ResponsibleName,
    string? ReponsibleCpf,
    int? ResponsibleKinship,
    string? ResponsibleNumber,
    string? EmergencyContactName,
    string? EmergencyContactNumber,
    int? EmergencyContactKinship,
    bool IsActive
);