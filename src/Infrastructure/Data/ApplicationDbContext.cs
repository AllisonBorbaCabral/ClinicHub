using Microsoft.EntityFrameworkCore;
using DemoMVC.Domain.Customers.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DemoMVC.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Customer>(e =>
        {
            e.ToTable("customers");

            e.HasKey(e => e.Id);

            e.Property(e => e.Id)
             .HasColumnName("id");

            e.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();

            e.Property(e => e.BirthDate)
                .HasColumnName("birthdate")
                .HasColumnType("date")
                .IsRequired();

            e.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            e.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            e.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
        });
    }
}

