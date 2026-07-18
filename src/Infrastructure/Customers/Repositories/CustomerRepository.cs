using DemoMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Domain.Customers.Entities;
using DemoMVC.Application.Customers.Interfaces;

namespace DemoMVC.Infrastructure.Customers.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .ToListAsync();
    }
    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == id);

        return customer;
    }
    public async Task<Customer?> GetByNameAsync(string name)
    {
        return await _context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name);
    }
    public async Task<Customer?> Create(Customer customer)
    {
        var response = await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return response.Entity;
    }
}