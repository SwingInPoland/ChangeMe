using System.Collections.Immutable;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Rules;

public class EditorsNumberMayNotExceed20Rule : IBusinessRule
{
    private readonly ImmutableHashSet<string> _editors;

    public EditorsNumberMayNotExceed20Rule(HashSet<string> editors)
    {
        _editors = editors.ToImmutableHashSet();
    }

    public bool IsBroken() => _editors.Count >= 20;

    public string Message => "The number of editors may not exceed 20.";
}