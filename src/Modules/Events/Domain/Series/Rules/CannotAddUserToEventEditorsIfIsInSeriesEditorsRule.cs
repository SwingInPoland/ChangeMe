using System.Collections.Immutable;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.Rules;

public class CannotAddUserToEventEditorsIfIsInSeriesEditorsRule : IBusinessRule
{
    private readonly ImmutableHashSet<string> _seriesEditors;
    private readonly ImmutableHashSet<string> _newUserIds;

    public CannotAddUserToEventEditorsIfIsInSeriesEditorsRule(HashSet<string> seriesEditors, HashSet<string> userIds)
    {
        _seriesEditors = seriesEditors.ToImmutableHashSet();
        _newUserIds = userIds.ToImmutableHashSet();
    }

    public bool IsBroken() => _newUserIds.Any(userId => _seriesEditors.Contains(userId));

    public string Message => "Cannot add user to event editors if they are already in series editors.";
}