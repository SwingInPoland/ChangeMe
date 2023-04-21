using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Rules;

public class CannotRemoveCreatorFromEditorsRule : IBusinessRule
{
    private readonly string _creatorId;
    private readonly string _userId;

    public CannotRemoveCreatorFromEditorsRule(string creatorId, string userId)
    {
        _creatorId = creatorId;
        _userId = userId;
    }

    public bool IsBroken() => _creatorId == _userId;

    public string Message => "The creator cannot be removed from the editors.";
}