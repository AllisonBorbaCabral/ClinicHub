using DemoMVC.Web.Interfaces.Customers;
using DemoMVC.Domain.Customers.Entities;
using DemoMVC.Application.Customers.DTOs;
using DemoMVC.Application.Customers.Interfaces;

namespace DemoMVC.Application.Customers.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<IReadOnlyList<GetCustomerDTO>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();

        return customers
            .Select(customer => new GetCustomerDTO
            (
                Id: customer.Id.ToString(),
                Name: customer.Name!,
                BirthDate: customer.BirthDate,
                CreatedAt: customer.CreatedAt,
                UpdatedAt: customer.UpdatedAt,
                IsActive: customer.IsActive
            ))
            .ToList();
    }
    public async Task<GetCustomerDTO?> GetByIdAsync(Guid id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        if (customer is null || string.IsNullOrEmpty(customer.Name))
            return null;

        return new GetCustomerDTO
        (
            Id: customer.Id.ToString(),
            Name: customer.Name,
            BirthDate: customer.BirthDate,
            CreatedAt: customer.CreatedAt,
            UpdatedAt: customer.UpdatedAt,
            IsActive: customer.IsActive
        );
    }
    public async Task<GetCustomerDTO?> GetByNameAsync(string name)
    {
        var customer = await _customerRepository.GetByNameAsync(name);

        if (customer is null || string.IsNullOrEmpty(customer.Name))
            return null;

        return new GetCustomerDTO
        (
            Id: customer.Id.ToString(),
            Name: customer.Name,
            BirthDate: customer.BirthDate,
            CreatedAt: customer.CreatedAt,
            UpdatedAt: customer.UpdatedAt,
            IsActive: customer.IsActive
        );
    }
    public async Task<GetCustomerDTO?> Create(CreateCustomerDTO request)
    {
        if (string.IsNullOrEmpty(request.Name))
            return null;

        var exists = await _customerRepository.GetByNameAsync(request.Name);

        if (exists is not null)
            return null;

        var customer = Customer.Create(
            request.Name,
            birthDate: request.BirthDate,
            isActive: request.IsActive);

        var customerCreated = await _customerRepository.Create(customer);

        if (customerCreated is null || string.IsNullOrEmpty(customerCreated.Name))
            return null;

        return new GetCustomerDTO(
            Id: customerCreated.Id.ToString(),
            Name: customerCreated.Name,
            BirthDate: customerCreated.BirthDate,
            CreatedAt: customerCreated.CreatedAt,
            UpdatedAt: customerCreated.UpdatedAt,
            IsActive: customer.IsActive
        );
    }
}