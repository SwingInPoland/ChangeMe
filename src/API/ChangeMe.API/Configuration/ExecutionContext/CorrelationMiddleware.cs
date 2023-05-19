using Microsoft.Extensions.Primitives;

namespace ChangeMe.API.Configuration.ExecutionContext;

internal class CorrelationMiddleware
{
    internal const string CorrelationHeaderKey = "CorrelationId";
    private readonly RequestDelegate _next;

    public CorrelationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = Guid.NewGuid().ToString();

        context.Request.Headers.Add(CorrelationHeaderKey, new StringValues(correlationId));

        await _next.Invoke(context);
    }
}