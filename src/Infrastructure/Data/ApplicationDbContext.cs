using Microsoft.EntityFrameworkCore;
using DemoMVC.Domain.Patients.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DemoMVC.Domain.People.Entities;

namespace DemoMVC.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext(options)
{
    public DbSet<Person> Persons => Set<Person>();
    // public DbSet<Patient> Patients => Set<Patient>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}

