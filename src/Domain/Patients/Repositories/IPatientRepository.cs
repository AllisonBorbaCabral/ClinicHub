using DemoMVC.Domain.Patients.Entities;
using DemoMVC.Domain.Patients.ValueObjects;

namespace DemoMVC.Domain.Patients.Repositories;

public interface IPatientRepository
{
    Task<IReadOnlyList<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(Guid id);
    Task<Patient?> GetByMedicalRecordAsync(MedicalRecord medicalRecord);
    Task<Patient?> GetForUpdateAsync(Guid id);
    Task<Patient?> Create(Patient patient);
}