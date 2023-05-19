using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Rules;

public class EditorsNumberMayNotExceed20Rule : IBusinessRule
{
    private readonly int _editors;

    public EditorsNumberMayNotExceed20Rule(HashSet<string> editors)
    {
        _editors = editors.Count;
    }

    public bool IsBroken() => _editors > 20;

    public string Message => "The number of editors may not exceed 20.";
}