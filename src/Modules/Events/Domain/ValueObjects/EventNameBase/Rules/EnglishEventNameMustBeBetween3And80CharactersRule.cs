using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase.Rules;

public class EnglishEventNameMustBeBetween3And80CharactersRule : IBusinessRule
{
    private readonly string _value;

    public EnglishEventNameMustBeBetween3And80CharactersRule(string value)
    {
        _value = value;
    }

    public bool IsBroken() => _value.Length is < 3 or > 80;

    public string Message => "Event name must be between 3 and 80 characters.";
}