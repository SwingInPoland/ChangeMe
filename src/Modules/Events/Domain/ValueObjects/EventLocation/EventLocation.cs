using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.ValueObjects;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation;

public class EventLocation : ValueObject
{
    public Coordinates Coordinates { get; }
    public City City { get; }
    public Province Province { get; }
    public PostalCode PostalCode { get; }
    public LocationName? Name { get; }
    public Street? Street { get; }

    private EventLocation(
        Coordinates coordinates,
        City city,
        Province province,
        PostalCode postalCode,
        LocationName? name,
        Street? street)
    {
        Coordinates = coordinates;
        City = city;
        Province = province;
        PostalCode = postalCode;
        Name = name;
        Street = street;
    }

    public static EventLocation Create(
        Coordinates coordinates,
        City city,
        Province province,
        PostalCode postalCode,
        LocationName? name,
        Street? street) => new(coordinates, city, province, postalCode, name, street);
}