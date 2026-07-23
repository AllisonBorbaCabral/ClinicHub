using Microsoft.EntityFrameworkCore;
using DemoMVC.Domain.Patients.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoMVC.Infrastructure.Patients.Mappings;

public class PatientMapping : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("patients");

        builder.HasKey(p => p.Id)
            .HasName("PK_patients");

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.Property(p => p.PersonId)
            .HasColumnName("person_id")
            .IsRequired(true);

        builder.OwnsOne(p => p.MedicalRecord, medical =>
        {
            medical.Property(m => m.Value)
                .HasColumnName("medical_record")
                .IsRequired(true);

            medical.HasIndex(m => m.Value)
                .HasDatabaseName("uq_patients_medical_record")
                .IsUnique(true);
        });

        builder.OwnsOne(p => p.Insurance, insurance =>
        {
            insurance.ToTable("patient_insurances");

            insurance.WithOwner()
                .HasForeignKey("patient_id")
                .HasConstraintName("FK_patient_insurances_patient");

            insurance.HasKey("patient_id")
                .HasName("PK_patient_insurances");

            insurance.Property(i => i.Value)
                .HasColumnName("insurance_id")
                .IsRequired(true);

            insurance.Property(i => i.DueDate)
                .HasColumnName("due_date")
                .IsRequired(true);
        });

        builder.Property(p => p.BloodType)
            .HasColumnName("blood_type")
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired(true);

        builder.OwnsOne(p => p.Responsible, responsible =>
        {
            responsible.ToTable("patient_responsibles");

            responsible.WithOwner()
                .HasForeignKey("person_id")
                .HasConstraintName("FK_patient_responsibles_patient");

            responsible.HasKey("person_id")
                .HasName("PK_patient_responsibles");

            responsible.OwnsOne(r => r.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsRequired(true);
            });

            responsible.OwnsOne(r => r.Cpf, cpf =>
            {
                cpf.Property(c => c.Value)
                    .HasColumnName("cpf")
                    .HasMaxLength(11)
                    .IsRequired(true);
            });

            responsible.Property(r => r.Kinship)
                .HasColumnName("kinship")
                .HasConversion<string>()
                .HasMaxLength(15)
                .IsRequired(true);

            responsible.OwnsOne(r => r.Number, number =>
            {
                number.Property(n => n.Value)
                    .HasColumnName("phone_number")
                    .IsRequired(true);
            });
        });

        builder.OwnsOne(p => p.EmergencyContact, emergency =>
        {
            emergency.ToTable("patient_emergency_contacts");

            emergency.WithOwner()
                .HasForeignKey("person_id")
                .HasConstraintName("FK_patient_emergency_contacts_patient");

            emergency.HasKey("person_id")
                .HasName("PK_patient_emergency_contacts");

            emergency.OwnsOne(e => e.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsRequired(true);
            });

            emergency.OwnsOne(e => e.Number, number =>
            {
                number.Property(n => n.Value)
                    .HasColumnName("phone_number")
                    .IsRequired(true);
            });

            emergency.Property(e => e.Kinship)
                .HasColumnName("kinship")
                .HasConversion<string>()
                .HasMaxLength(15)
                .IsRequired(true);
        });

        builder.HasOne(p => p.Person)
            .WithOne(p => p.Patient)
            .HasForeignKey<Patient>(p => p.PersonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}