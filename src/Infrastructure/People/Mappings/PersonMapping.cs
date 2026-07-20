using Microsoft.EntityFrameworkCore;
using DemoMVC.Domain.People.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoMVC.Infrastructure.People.Mappings;

public class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("people");

        builder.HasKey(p => p.Id)
            .HasName("PK_people");

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired(true);
        });

        builder.OwnsOne(p => p.BirthDate, birthdate =>
        {
            birthdate.Property(b => b.Value)
                .HasColumnName("birthdate")
                .IsRequired(true);
        });

        builder.OwnsOne(p => p.Cpf, cpf =>
        {
            cpf.Property(c => c.Value)
                .HasColumnName("cpf")
                .HasMaxLength(11)
                .IsRequired(true);
        });

        builder.Property(p => p.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(p => p.IsDeleted)
            .HasColumnName("is_deleted")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(p => p.DeletedAt)
            .HasColumnName("deleted_at");

        builder.OwnsOne(p => p.Address, address =>
        {
            address.ToTable("people_addresses");

            address.WithOwner()
                .HasForeignKey("person_id")
                .HasConstraintName("FK_people_addresses_people");

            address.HasKey("person_id")
                .HasName("PK_people_addresses");

            address.OwnsOne(x => x.Street, street =>
            {
                street.Property(s => s.Value)
                    .HasColumnName("street")
                    .HasMaxLength(200)
                    .IsRequired(true);
            });

            address.OwnsOne(x => x.Number, number =>
            {
                number.Property(n => n.Value)
                    .HasColumnName("number")
                    .HasMaxLength(10)
                    .IsRequired(true);
            });

            address.OwnsOne(x => x.Neighborhood, neighborhood =>
            {
                neighborhood.Property(n => n.Value)
                    .HasColumnName("neighborhood")
                    .HasMaxLength(200)
                    .IsRequired(true);
            });

            address.OwnsOne(x => x.PostalCode, postal =>
            {
                postal.Property(p => p.Value)
                    .HasColumnName("postal_code")
                    .HasMaxLength(8)
                    .IsRequired(true);
            });

            address.OwnsOne(x => x.AddressLine, addressline =>
            {
                addressline.Property(ad => ad.Value)
                    .HasColumnName("address_line")
                    .HasMaxLength(100)
                    .IsRequired(false);
            });
        });

        builder.OwnsOne(p => p.Contact, contact =>
        {
            contact.ToTable("people_contacts");

            contact.WithOwner()
                .HasForeignKey("person_id")
                .HasConstraintName("FK_people_contacts_people");

            contact.HasKey("person_id")
                .HasName("PK_people_contacts");

            contact.OwnsOne(c => c.Phone, phone =>
            {
                phone.Property(p => p.Value)
                    .HasColumnName("phone_number")
                    .IsRequired(false);
            });

            contact.OwnsOne(c => c.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsRequired(false);
            });
        });
    }
}