using DemoMVC.Shared.Results;
using DemoMVC.Shared.Domain.Entities;
using DemoMVC.Domain.People.ValueObjects;

namespace DemoMVC.Domain.People.Entities;

public class Address : Base
{
    public Street Street { get; private set; } = null!;
    public StreetNumber Number { get; private set; } = null!;
    public Neighborhood Neighborhood { get; private set; } = null!;
    public AddressLine AddressLine { get; private set; } = null!;
    public PostalCode PostalCode { get; private set; } = null!;
    private Address() { }
    private Address(
        Street street,
        StreetNumber streetNumber,
        Neighborhood neighborhood,
        AddressLine addressLine,
        PostalCode postalCode)
    {
        Id = Guid.NewGuid();
        Street = street;
        Number = streetNumber;
        Neighborhood = neighborhood;
        AddressLine = addressLine;
        PostalCode = postalCode;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
        IsDeleted = false;
    }
    public static Result<Address> Create(
        string street,
        string streetNumber,
        string neighborhood,
        string addressLine,
        string postalCode)
    {
        var streetObj = Street.Create(street).Data;
        if (streetObj == null)
            return Result<Address>.Fail("Falha ao criar a rua");

        var numberObj = StreetNumber.Create(streetNumber).Data;
        if (numberObj == null)
            return Result<Address>.Fail("Falha ao criar o número");

        var neighborhoodObj = Neighborhood.Create(neighborhood).Data;
        if (neighborhoodObj == null)
            return Result<Address>.Fail("Falha ao criar o Bairro");

        var addressLineObj = AddressLine.Create(addressLine).Data;
        if (addressLineObj == null)
            return Result<Address>.Fail("Falha ao criar o Complemento");

        var postalCodeObj = PostalCode.Create(postalCode).Data;
        if (postalCodeObj == null)
            return Result<Address>.Fail("Falha ao criar o CEP");

        return Result<Address>.Ok(new Address(
            streetObj,
            numberObj,
            neighborhoodObj,
            addressLineObj,
            postalCodeObj
        ));
    }
}