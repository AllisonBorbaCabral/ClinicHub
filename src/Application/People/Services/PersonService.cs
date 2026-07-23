using DemoMVC.Shared.Results;
using DemoMVC.Domain.People.Entities;
using DemoMVC.Application.People.DTOs;
using DemoMVC.Domain.People.Repositories;
using DemoMVC.Shared.Domain.ValueObjects;
using DemoMVC.Application.People.Interfaces;

namespace DemoMVC.Application.People.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    private readonly IPersonUniquenessCheckerRepository _checker;
    public PersonService(IPersonRepository repository, IPersonUniquenessCheckerRepository checker)
    {
        _repository = repository;
        _checker = checker;
    }
    public async Task<Result<IReadOnlyList<ListPersonDTO>>> GetAllAsync()
    {
        var people = await _repository.GetAllAsync();

        return Result<IReadOnlyList<ListPersonDTO>>.Ok(people
            .Select(person => new ListPersonDTO
            (
                Id: person.Id.ToString(),
                Name: person.Name.Value,
                BirthDate: person.BirthDate.Value,
                Cpf: person.Cpf.Value,
                IsActive: person.IsActive
            ))
            .ToList());
    }
    public async Task<Result<DetailPersonDTO>> GetForUpdateAsync(Guid id)
    {
        var person = await _repository.GetForUpdateAsync(id);

        if (person is null)
            return Result<DetailPersonDTO>.Fail("Registro não encontrado.");

        return Result<DetailPersonDTO>.Ok(new DetailPersonDTO(
            Id: person.Id.ToString(),
            Name: person.Name.Value,
            BirthDate: person.BirthDate.Value,
            Cpf: person.Cpf.Value,
            Street: person.Address?.Street.Value,
            StreetNumber: person.Address?.Number.Value,
            Neighborhood: person.Address?.Neighborhood.Value,
            AddressLine: person.Address?.AddressLine?.Value,
            PostalCode: person.Address?.PostalCode.Value,
            PhoneNumber: person.Contact?.Phone?.Value,
            Email: person.Contact?.Email?.Value,
            IsActive: person.IsActive,
            IsDeleted: person.IsDeleted,
            CreatedAt: person.CreatedAt,
            UpdatedAt: person.UpdatedAt,
            DeletedAt: person.DeletedAt
        ));
    }
    public async Task<Result<Guid>> Create(CreatePersonDTO request)
    {
        var cpf = Cpf.Create(request.Cpf);

        if (await _checker.ExistsByCpfAsync(cpf.Data!))
            return Result<Guid>.Fail("Já existe um registro com esse CPF.");

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
            request.Email);

        if (!person.Success)
            return Result<Guid>.Fail("Falha ao criar pessoa.");

        await _repository.Create(person.Data!);

        return Result<Guid>.Ok(person.Data!.Id);
    }
}