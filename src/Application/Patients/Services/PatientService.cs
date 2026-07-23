using DemoMVC.Shared.Results;
using DemoMVC.Domain.Patients.Entities;
using DemoMVC.Application.Patients.DTOs;
using DemoMVC.Domain.People.Repositories;
using DemoMVC.Domain.Patients.Repositories;
using DemoMVC.Application.Patients.Interfaces;
using DemoMVC.Shared.Domain.ValueObjects;
using DemoMVC.Domain.People.Entities;

namespace DemoMVC.Application.Patients.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;
    private readonly IMedicalRecordGenerator _generator;
    private readonly IPersonUniquenessCheckerRepository _checker;
    public PatientService(
        IPatientRepository repository,
        IMedicalRecordGenerator generator,
        IPersonUniquenessCheckerRepository checker)
    {
        _repository = repository;
        _generator = generator;
        _checker = checker;
    }
    public async Task<Result<IReadOnlyList<ListPatientDTO>>> GetAllAsync()
    {
        var patients = await _repository.GetAllAsync();

        return Result<IReadOnlyList<ListPatientDTO>>.Ok(patients
            .Select(patient => new ListPatientDTO
            (
                Id: patient.Id.ToString(),
                MedicalRecord: patient.MedicalRecord.Value,
                Name: patient.Person.Name.Value,
                BirthDate: patient.Person.BirthDate.Value,
                Cpf: patient.Person.Cpf.Value,
                InsuranceId: patient.Insurance?.Value,
                InsuranceDueDate: patient.Insurance?.DueDate,
                BloodType: patient.BloodType.ToString(),
                Responsible: patient.Responsible?.Name.Value,
                EmergencyContact: patient.EmergencyContact?.Name.Value,
                IsActive: patient.IsActive
            ))
            .ToList());
    }
    public async Task<Result<DetailPatientDTO>> GetForUpdateAsync(Guid id)
    {
        var patient = await _repository.GetForUpdateAsync(id);

        if (patient is null)
            return Result<DetailPatientDTO>.Fail("Não foram encontrados registros.");

        return Result<DetailPatientDTO>.Ok(new DetailPatientDTO
        (
            Id: patient.Id.ToString(),
            PersonId: patient.Person.Id.ToString(),
            MedicalRecord: patient.MedicalRecord.Value,
            Name: patient.Person.Name.Value,
            BirthDate: patient.Person.BirthDate.Value,
            Cpf: patient.Person.Cpf.Value,
            Street: patient.Person.Address?.Street.Value,
            StreetNumber: patient.Person.Address?.Number.Value,
            Neighborhood: patient.Person.Address?.Neighborhood.Value,
            AddressLine: patient.Person.Address?.AddressLine?.Value,
            PostalCode: patient.Person.Address?.PostalCode.Value,
            PhoneNumber: patient.Person.Contact?.Phone?.Value,
            Email: patient.Person.Contact?.Email?.Value,
            IsActivePerson: patient.Person.IsActive,
            InsuranceId: patient.Insurance?.Value,
            InsuranceDueDate: patient.Insurance?.DueDate,
            BloodType: patient.BloodType.ToString(),
            ResponsibleName: patient.Responsible?.Name.Value,
            ReponsibleCpf: patient.Responsible?.Cpf.Value,
            ResponsibleKinship: patient.Responsible?.Kinship.ToString(),
            ResponsibleNumber: patient.Responsible?.Number.Value,
            EmergencyContactName: patient.EmergencyContact?.Name.Value,
            EmergencyContactKinship: patient.EmergencyContact?.Kinship.ToString(),
            EmergencyContactNumber: patient.EmergencyContact?.Number.Value,
            IsActive: patient.IsActive
        ));
    }
    public async Task<Result<Guid?>> Create(CreatePatientDTO request)
    {
        if (string.IsNullOrEmpty(request.Name))
            return Result<Guid?>.Fail("Nome não pode ser nulo ou vazio.");

        if (string.IsNullOrEmpty(request.Cpf))
            return Result<Guid?>.Fail("CPF não pode ser nulo ou vazio.");

        var cpf = Cpf.Create(request.Cpf);

        if (!cpf.Success)
            return Result<Guid?>.Fail("Falha ao criar o CPF.");

        if (await _checker.ExistsByCpfAsync(cpf.Data!))
            return Result<Guid?>.Fail("Já existe um registro com esse CPF.");


        var person = Person.Create(
            request.Name,
            request.BirthDate,
            cpf.Data!.Value,
            request.Street,
            request.StreetNumber,
            request.Neighborhood,
            request.AddressLine,
            request.PostalCode,
            request.PhoneNumber,
            request.Email
        );

        var medicalRecord = await _generator.NextAsync();

        var patient = Patient.Create(
            medicalRecord.Value,
            person.Data!,
            request.InsuranceId,
            request.InsuranceDueDate,
            request.BloodType,
            request.ResponsibleName,
            request.ReponsibleCpf,
            request.ResponsibleKinship,
            request.ResponsibleNumber,
            request.EmergencyContactName,
            request.EmergencyContactNumber,
            request.EmergencyContactKinship);

        if (!patient.Success)
            return Result<Guid?>.Fail("Não foi possivel criar o paciente.");

        var response = await _repository.Create(patient.Data!);
        if (response == null)
            return Result<Guid?>.Fail("Ocorreu um erro ao criar o paciente.");
        return Result<Guid?>.Ok(response.Id);
    }
}