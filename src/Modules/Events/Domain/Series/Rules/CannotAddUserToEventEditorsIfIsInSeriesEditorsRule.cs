using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.Rules;

public class CannotAddUserToEventEditorsIfIsInSeriesEditorsRule : IBusinessRule
{
    private readonly IReadOnlyCollection<string> _seriesEditors;
    private readonly string _newUserId;

    public CannotAddUserToEventEditorsIfIsInSeriesEditorsRule(
        IReadOnlyCollection<string> seriesEditors,
        string newUserId)
    {
        _seriesEditors = seriesEditors;
        _newUserId = newUserId;
    }

    public bool IsBroken() => _seriesEditors.Contains(_newUserId);

    public string Message => "Cannot add user to event editors if they are already in series editors.";
}