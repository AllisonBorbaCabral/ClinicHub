using DemoMVC.Application.Customers.DTOs;

namespace DemoMVC.Web.Interfaces.Customers;

public interface ICustomerService
{
    Task<IReadOnlyList<GetCustomerDTO>> GetAllAsync();
    Task<GetCustomerDTO?> GetByIdAsync(Guid id);
    Task<GetCustomerDTO?> GetByNameAsync(string name);
    // Task<CustomerViewModel?> GetByDocument(string document);
    Task<GetCustomerDTO?> Create(CreateCustomerDTO request);
}