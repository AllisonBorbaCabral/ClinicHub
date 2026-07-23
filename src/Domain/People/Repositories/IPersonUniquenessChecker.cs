using DemoMVC.Domain.People.Entities;
using DemoMVC.Shared.Domain.ValueObjects;

namespace DemoMVC.Domain.People.Repositories;

public interface IPersonUniquenessCheckerRepository
{
    Task<bool> ExistsByCpfAsync(Cpf cpf);
    Task<bool> ExistsByEmailAsync(Email email);
}