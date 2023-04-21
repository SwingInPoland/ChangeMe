using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.ValueObjects;

public class Coordinates : ValueObject
{
    public float Latitude { get; }
    public float Longitude { get; }

    private Coordinates(float latitude, float longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Coordinates Create(float latitude, float longitude)
    {
        CheckRule(new EventLatitudeMustBeValidRule(latitude));
        CheckRule(new EventLongitudeMustBeValidRule(longitude));

        return new Coordinates(latitude, longitude);
    }
}