using DemoMVC.Domain.People.Entities;
using DemoMVC.Shared.Domain.ValueObjects;

namespace DemoMVC.Domain.People.Repositories;

public interface IPersonRepository
{
    Task<IReadOnlyList<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(Guid id);
    Task<Person?> GetByNameAsync(PersonName name);
    Task<Person?> GetByCpfAsync(Cpf cpf);
    Task<Person?> GetForUpdateAsync(Guid id);
    Task<Person?> Create(Person person);
}