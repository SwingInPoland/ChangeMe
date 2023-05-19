using ChangeMe.Shared.Application;

namespace ChangeMe.API.Configuration.ExecutionContext;

public class ExecutionContextAccessor : IExecutionContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId => _httpContextAccessor
        .HttpContext?
        .User
        .Claims
        .SingleOrDefault(x => x.Type == "sub")?
        .Value is not null
        ? _httpContextAccessor.HttpContext.User.Claims.Single(x => x.Type == "sub").Value
        : throw new ApplicationException("User context is not available");

    public Guid CorrelationId => IsAvailable && _httpContextAccessor.HttpContext!.Request.Headers.TryGetValue(
        CorrelationMiddleware.CorrelationHeaderKey, out var correlationId)
        ? Guid.Parse(correlationId!)
        : throw new ApplicationException("Http context and correlation id is not available");

    public bool IsAvailable => _httpContextAccessor.HttpContext is not null;
}