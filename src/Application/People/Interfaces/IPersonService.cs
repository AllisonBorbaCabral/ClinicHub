using DemoMVC.Application.People.DTOs;
using DemoMVC.Shared.Results;

namespace DemoMVC.Application.People.Interfaces;

public interface IPersonService
{
    Task<Result<IReadOnlyList<ListPersonDTO>>> GetAllAsync();
    Task<Result<DetailPersonDTO>> GetForUpdateAsync(Guid id);
    Task<Result<Guid>> Create(CreatePersonDTO request);
}