using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class ProvinceMustExistsRule : IBusinessRule
{
    private static readonly string[] ExistingProvinces =
    {
        "DOLNOSLASKIE",
        "KUJAWSKO-POMORSKIE",
        "LUBELSKIE",
        "LUBUSKIE",
        "LODZKIE",
        "MALOPOLSKIE",
        "MAZOWIECKIE",
        "OPOLSKIE",
        "PODKARPACKIE",
        "PODLASKIE",
        "POMORSKIE",
        "SLASKIE",
        "SWIETOKRZYSKIE",
        "WARMINSKO-MAZURSKIE",
        "WIELKOPOLSKIE",
        "ZACHODNIOPOMORSKIE"
    };

    private readonly string _provinceName;

    public ProvinceMustExistsRule(string provinceName)
    {
        _provinceName = provinceName;
    }

    public bool IsBroken() => ExistingProvinces.Contains(_provinceName);

    public string Message => $"Province with name {_provinceName} does not exist in Poland.";
}