using ChangeMe.Shared.Extensions;

namespace ChangeMe.Shared.Application;

public class InvalidCommandException : Exception
{
    public IReadOnlyCollection<string> Errors { get; }

    public InvalidCommandException(IEnumerable<string> errors)
    {
        Errors = errors.ToReadOnly();
    }
}