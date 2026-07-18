using DemoMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Domain.Patients.Entities;
using DemoMVC.Domain.Patients.Repositories;

namespace DemoMVC.Infrastructure.Patients.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Patient>> GetAllAsync()
    {
        return await _context.Patients
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .ToListAsync();
    }
    public async Task<Patient?> GetByIdAsync(Guid id)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(c => c.Id == id);

        return patient;
    }
    public async Task<Patient?> GetByNameAsync(string name)
    {
        return await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name);
    }
    public async Task<Patient?> Create(Patient patient)
    {
        var response = await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();

        return response.Entity;
    }
}