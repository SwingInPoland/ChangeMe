using System.Collections.Immutable;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Rules;

public class CannotAddUserToEditorsTwiceRule : IBusinessRule
{
    private readonly ImmutableHashSet<string> _editors;
    private readonly string _userId;

    public CannotAddUserToEditorsTwiceRule(HashSet<string> editors, string userId)
    {
        _editors = editors.ToImmutableHashSet();
        _userId = userId;
    }

    public bool IsBroken() => _editors.Contains(_userId);

    public string Message => "User cannot be added to editors twice.";
}