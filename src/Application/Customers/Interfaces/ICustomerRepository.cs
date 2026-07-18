using DemoMVC.Domain.Customers.Entities;

namespace DemoMVC.Application.Customers.Interfaces;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer?> GetByNameAsync(string name);
    Task<Customer?> Create(Customer customer);
}