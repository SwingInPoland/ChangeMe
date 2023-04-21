using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNames.Rules;

public class UkrainianEventNameMustBeBetween3And90CharactersRule : IBusinessRule
{
    private readonly string _value;

    public UkrainianEventNameMustBeBetween3And90CharactersRule(string value)
    {
        _value = value;
    }

    public bool IsBroken() => _value.Length is < 3 or > 90;

    public string Message => "Ukrainian event name must be between 3 and 90 characters.";
}