using ChangeMe.Shared.Application;
using Microsoft.AspNetCore.Mvc;

namespace ChangeMe.API.Configuration.Validation;

public class InvalidCommandProblemDetails : ProblemDetails
{
    public List<string> Errors { get; }

    public InvalidCommandProblemDetails(InvalidCommandException exception)
    {
        Title = "Command validation error";
        Status = StatusCodes.Status400BadRequest;
        Type = "https://somedomain/validation-error";
        Errors = exception.Errors.ToList();
    }
}