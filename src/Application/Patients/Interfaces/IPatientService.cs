using DemoMVC.Application.Patients.DTOs;

namespace DemoMVC.Application.Patients.Interfaces;

public interface IPatientService
{
    Task<IReadOnlyList<GetPatientDTO>> GetAllAsync();
    Task<GetPatientDTO?> GetByIdAsync(Guid id);
    Task<GetPatientDTO?> GetByNameAsync(string name);
    // Task<CustomerViewModel?> GetByDocument(string document);
    Task<GetPatientDTO?> Create(CreatePatientDTO request);
}