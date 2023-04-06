namespace ChangeMe.Shared.Domain;

public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
}