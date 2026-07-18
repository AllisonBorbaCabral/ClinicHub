using DemoMVC.Domain.Patients.Entities;
using DemoMVC.Domain.Patients.Repositories;
using DemoMVC.Application.Patients.DTOs;
using DemoMVC.Application.Patients.Interfaces;

namespace DemoMVC.Application.Patients.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    public async Task<IReadOnlyList<GetPatientDTO>> GetAllAsync()
    {
        var patients = await _patientRepository.GetAllAsync();

        return patients
            .Select(patient => new GetPatientDTO
            (
                Id: patient.Id.ToString(),
                Name: patient.Name!,
                BirthDate: patient.BirthDate,
                CreatedAt: patient.CreatedAt,
                UpdatedAt: patient.UpdatedAt,
                IsActive: patient.IsActive
            ))
            .ToList();
    }
    public async Task<GetPatientDTO?> GetByIdAsync(Guid id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);

        if (patient is null || string.IsNullOrEmpty(patient.Name))
            return null;

        return new GetPatientDTO
        (
            Id: patient.Id.ToString(),
            Name: patient.Name,
            BirthDate: patient.BirthDate,
            CreatedAt: patient.CreatedAt,
            UpdatedAt: patient.UpdatedAt,
            IsActive: patient.IsActive
        );
    }
    public async Task<GetPatientDTO?> GetByNameAsync(string name)
    {
        var patient = await _patientRepository.GetByNameAsync(name);

        if (patient is null || string.IsNullOrEmpty(patient.Name))
            return null;

        return new GetPatientDTO
        (
            Id: patient.Id.ToString(),
            Name: patient.Name,
            BirthDate: patient.BirthDate,
            CreatedAt: patient.CreatedAt,
            UpdatedAt: patient.UpdatedAt,
            IsActive: patient.IsActive
        );
    }
    public async Task<GetPatientDTO?> Create(CreatePatientDTO request)
    {
        if (string.IsNullOrEmpty(request.Name))
            return null;

        var exists = await _patientRepository.GetByNameAsync(request.Name);

        if (exists is not null)
            return null;

        var patient = Patient.Create(
            request.Name,
            birthDate: request.BirthDate,
            isActive: request.IsActive);

        var patientCreated = await _patientRepository.Create(patient);

        if (patientCreated is null || string.IsNullOrEmpty(patientCreated.Name))
            return null;

        return new GetPatientDTO(
            Id: patientCreated.Id.ToString(),
            Name: patientCreated.Name,
            BirthDate: patientCreated.BirthDate,
            CreatedAt: patientCreated.CreatedAt,
            UpdatedAt: patientCreated.UpdatedAt,
            IsActive: patientCreated.IsActive
        );
    }
}