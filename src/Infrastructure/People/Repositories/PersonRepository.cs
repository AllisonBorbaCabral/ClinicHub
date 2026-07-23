using DemoMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Domain.People.Entities;
using DemoMVC.Domain.People.Repositories;
using DemoMVC.Shared.Domain.ValueObjects;

namespace DemoMVC.Infrastructure.People.Repositories;

public class PersonRepository : IPersonRepository, IPersonUniquenessCheckerRepository
{
    private readonly ApplicationDbContext _context;

    public PersonRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<Person>> GetAllAsync()
    {
        return await _context.Persons
            .AsNoTracking()
            .Include(p => p.Address)
            .Include(p => p.Contact)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }
    public async Task<Person?> GetByIdAsync(Guid id)
    {
        return await _context.Persons
            .AsNoTracking()
            .Include(p => p.Address)
            .Include(p => p.Contact)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Person?> GetByNameAsync(PersonName name)
    {
        return await _context.Persons
            .AsNoTracking()
            .Include(p => p.Address)
            .Include(p => p.Contact)
            .FirstOrDefaultAsync(p => p.Name == name);
    }
    public async Task<Person?> GetByCpfAsync(Cpf cpf)
    {
        return await _context.Persons
            .AsNoTracking()
            .Include(p => p.Address)
            .Include(p => p.Contact)
            .FirstOrDefaultAsync(p => p.Cpf == cpf);
    }
    public async Task<Person?> GetForUpdateAsync(Guid id)
    {
        return await _context.Persons
            .Include(p => p.Address)
            .Include(p => p.Contact)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Person?> Create(Person person)
    {
        var response = await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();

        return response.Entity;
    }
    public async Task<bool> ExistsByCpfAsync(Cpf cpf)
    {
        return await _context.Persons
            .AsNoTracking()
            .AnyAsync(p => p.Cpf == cpf);
    }
    public async Task<bool> ExistsByEmailAsync(Email email)
    {
        return await _context.Persons
            .AsNoTracking()
            .AnyAsync(p => p.Contact!.Email == email);
    }
}