namespace DemoMVC.Application.Patients.DTOs;

public sealed record DetailPatientDTO(
    string Id,
    string PersonId,
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
    bool IsActivePerson,
    int MedicalRecord,
    string? InsuranceId,
    DateOnly? InsuranceDueDate,
    string BloodType,
    string? ResponsibleName,
    string? ReponsibleCpf,
    string? ResponsibleKinship,
    string? ResponsibleNumber,
    string? EmergencyContactName,
    string? EmergencyContactNumber,
    string? EmergencyContactKinship,
    bool IsActive
);