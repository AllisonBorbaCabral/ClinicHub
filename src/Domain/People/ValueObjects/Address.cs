using DemoMVC.Shared.Results;

namespace DemoMVC.Domain.People.ValueObjects;

public sealed class Address
{
    public Street Street { get; private set; } = null!;
    public StreetNumber Number { get; private set; } = null!;
    public Neighborhood Neighborhood { get; private set; } = null!;
    public AddressLine? AddressLine { get; private set; } = null!;
    public PostalCode PostalCode { get; private set; } = null!;
    private Address() { }
    private Address(
        Street street,
        StreetNumber streetNumber,
        Neighborhood neighborhood,
        AddressLine? addressLine,
        PostalCode postalCode)
    {
        Street = street;
        Number = streetNumber;
        Neighborhood = neighborhood;
        AddressLine = addressLine;
        PostalCode = postalCode;
    }
    public static Result<Address> Create(
        string street,
        string streetNumber,
        string neighborhood,
        string? addressLine,
        string postalCode)
    {
        var streetObj = Street.Create(street);
        if (!streetObj.Success || streetObj.Data == null)
            return Result<Address>.Fail("Falha ao criar a rua");

        var numberObj = StreetNumber.Create(streetNumber);
        if (!numberObj.Success || numberObj.Data == null)
            return Result<Address>.Fail("Falha ao criar o número");

        var neighborhoodObj = Neighborhood.Create(neighborhood);
        if (!neighborhoodObj.Success || neighborhoodObj.Data == null)
            return Result<Address>.Fail("Falha ao criar o Bairro");


        var addressLineObj = AddressLine.Create(addressLine);
        if (!addressLineObj.Success || addressLineObj.Data == null)
            return Result<Address>.Fail("Falha ao criar o Complemento");

        var postalCodeObj = PostalCode.Create(postalCode);
        if (!postalCodeObj.Success || postalCodeObj.Data == null)
            return Result<Address>.Fail("Falha ao criar o CEP");

        return Result<Address>.Ok(new Address(
            streetObj.Data,
            numberObj.Data,
            neighborhoodObj.Data,
            addressLineObj.Data,
            postalCodeObj.Data
        ));
    }
}