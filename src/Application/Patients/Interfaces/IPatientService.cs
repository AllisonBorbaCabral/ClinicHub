using DemoMVC.Application.Patients.DTOs;
using DemoMVC.Shared.Results;

namespace DemoMVC.Application.Patients.Interfaces;

public interface IPatientService
{
    Task<Result<IReadOnlyList<ListPatientDTO>>> GetAllAsync();
    Task<Result<DetailPatientDTO>> GetForUpdateAsync(Guid id);
    // Task<Result<GetPatientDTO?>> GetByNameAsync(string name);
    // Task<CustomerViewModel?> GetByDocument(string document);
    Task<Result<Guid?>> Create(CreatePatientDTO request);
}