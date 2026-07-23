namespace DemoMVC.Application.Patients.DTOs;

public sealed record ListPatientDTO(
    string Id,
    int MedicalRecord,
    string Name,
    DateOnly BirthDate,
    string Cpf,
    string? InsuranceId,
    DateOnly? InsuranceDueDate,
    string BloodType,
    string? Responsible,
    string? EmergencyContact,
    bool IsActive
);