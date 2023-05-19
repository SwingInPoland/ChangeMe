using System.Collections.Immutable;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Rules;

public class CreatorMustBeInEditorsRule : IBusinessRule
{
    private readonly string _creatorId;
    private readonly ImmutableHashSet<string> _userIds;

    public CreatorMustBeInEditorsRule(string creatorId, HashSet<string> userIds)
    {
        _creatorId = creatorId;
        _userIds = userIds.ToImmutableHashSet();
    }

    public bool IsBroken() => _userIds.Contains(_creatorId);

    public string Message => "The creator must be in the editors.";
}