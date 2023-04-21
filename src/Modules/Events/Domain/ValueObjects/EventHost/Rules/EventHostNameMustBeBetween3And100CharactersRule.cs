using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventHost.Rules;

public class EventHostNameMustBeBetween3And100CharactersRule : IBusinessRule
{
    private readonly string _name;

    public EventHostNameMustBeBetween3And100CharactersRule(string name)
    {
        _name = name;
    }

    public bool IsBroken() => string.IsNullOrWhiteSpace(_name) || _name.Length is < 3 or > 100;

    public string Message => "Event host name must be between 3 and 100 characters long.";
}