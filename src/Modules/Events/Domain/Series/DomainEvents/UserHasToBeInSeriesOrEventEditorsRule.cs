using System.Collections.Immutable;
using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class UserHasToBeInSeriesOrEventEditorsRule : IBusinessRule
{
    private readonly ImmutableHashSet<string> _seriesEditors;
    private readonly ImmutableHashSet<string> _eventEditors;
    private readonly string _userId;

    public UserHasToBeInSeriesOrEventEditorsRule(
        HashSet<string> seriesEditors,
        HashSet<string> eventEditors,
        string userId)
    {
        _seriesEditors = seriesEditors.ToImmutableHashSet();
        _eventEditors = eventEditors.ToImmutableHashSet();
        _userId = userId;
    }

    public bool IsBroken() => _seriesEditors.NotContains(_userId) && _eventEditors.NotContains(_userId);

    public string Message => "The user is not in the editors.";
}