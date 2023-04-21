using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNames.Rules;

public class PolishEventNameMustBeBetween3And100CharactersRule : IBusinessRule
{
    private readonly string _value;

    public PolishEventNameMustBeBetween3And100CharactersRule(string value)
    {
        _value = value;
    }

    public bool IsBroken() => _value.Length is < 3 or > 100;

    public string Message => "Polish event name must be between 3 and 100 characters.";
}