using DemoMVC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Domain.People.Entities;
using DemoMVC.Domain.Patients.Entities;
using DemoMVC.Domain.People.Repositories;
using DemoMVC.Shared.Domain.ValueObjects;
using DemoMVC.Domain.Patients.Repositories;

namespace DemoMVC.Infrastructure.Patients.Repositories;

public class PatientRepository : IPatientRepository, IPersonUniquenessCheckerRepository
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
            .Include(p => p.Person)
                .ThenInclude(p => p.Address)
            .Include(p => p.Person)
                .ThenInclude(p => p.Contact)
            .Include(p => p.Insurance)
            .Include(p => p.Responsible)
            .Include(p => p.EmergencyContact)
            .OrderBy(p => p.MedicalRecord)
            .ToListAsync();
    }
    public async Task<Patient?> GetByIdAsync(Guid id)
    {
        return await _context.Patients
            .AsNoTracking()
            .Include(p => p.Person)
                .ThenInclude(p => p.Address)
            .Include(p => p.Person)
                .ThenInclude(p => p.Contact)
            .Include(p => p.Insurance)
            .Include(p => p.Responsible)
            .Include(p => p.EmergencyContact)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Patient?> GetByMedicalRecordAsync(int medicalRecord)
    {
        return await _context.Patients
            .AsNoTracking()
            .Include(p => p.Person)
                .ThenInclude(p => p.Address)
            .Include(p => p.Person)
                .ThenInclude(p => p.Contact)
            .Include(p => p.Insurance)
            .Include(p => p.Responsible)
            .Include(p => p.EmergencyContact)
            .FirstOrDefaultAsync(p => p.MedicalRecord == medicalRecord);
    }
    public async Task<Patient?> GetForUpdateAsync(Guid id)
    {
        return await _context.Patients
            .Include(p => p.Person)
                .ThenInclude(p => p.Address)
            .Include(p => p.Person)
                .ThenInclude(p => p.Contact)
            .Include(p => p.Insurance)
            .Include(p => p.Responsible)
            .Include(p => p.EmergencyContact)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Patient?> Create(Patient patient)
    {
        var response = await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();

        return response.Entity;
    }
    public async Task<bool> ExistsByCpfAsync(Cpf cpf)
    {
        return await _context.Patients
            .AsNoTracking()
            .AnyAsync(p => p.Person.Cpf == cpf);
    }
    public async Task<bool> ExistsByEmailAsync(Email email)
    {
        return await _context.Patients
            .AsNoTracking()
            .AnyAsync(p => p.Person.Contact!.Email == email);
    }
}