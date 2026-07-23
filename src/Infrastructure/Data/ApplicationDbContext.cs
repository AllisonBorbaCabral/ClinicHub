using Microsoft.EntityFrameworkCore;
using DemoMVC.Domain.People.Entities;
using DemoMVC.Domain.Patients.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DemoMVC.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext(options)
{
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Patient> Patients => Set<Patient>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        ConfigureSequences(builder);
    }
    private static void ConfigureSequences(ModelBuilder builder)
    {
        builder.HasSequence<int>("medical_record_sequence")
            .StartsAt(1)
            .IncrementsBy(1);

        builder.HasSequence<int>("appointment_sequence")
        .StartsAt(1)
        .IncrementsBy(1);

    }
}

