using DemoMVC.Domain.Patients.Entities;

namespace DemoMVC.Domain.Patients.Repositories;

public interface IPatientRepository
{
    Task<IReadOnlyList<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(Guid id);
    Task<Patient?> GetByNameAsync(string name);
    Task<Patient?> Create(Patient patient);
}