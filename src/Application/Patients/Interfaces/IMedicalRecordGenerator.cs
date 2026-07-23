using DemoMVC.Domain.Patients.ValueObjects;

namespace DemoMVC.Application.Patients.Interfaces;

public interface IMedicalRecordGenerator
{
    Task<MedicalRecord> NextAsync();
}