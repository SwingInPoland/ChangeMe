using System.Collections.Immutable;
using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.Rules;

public class UserHasToBeInEditorsRule : IBusinessRule
{
    private readonly ImmutableHashSet<string> _editors;
    private readonly string _userId;

    public UserHasToBeInEditorsRule(HashSet<string> editors, string userId)
    {
        _editors = editors.ToImmutableHashSet();
        _userId = userId;
    }

    public bool IsBroken() => _editors.NotContains(_userId);

    public string Message => "The user is not in the editors.";
}