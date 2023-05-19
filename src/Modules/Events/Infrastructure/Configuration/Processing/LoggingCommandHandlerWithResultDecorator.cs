using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Shared.Application;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing;

internal class LoggingCommandHandlerWithResultDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    private readonly ILogger _logger;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly ICommandHandler<TCommand, TResult> _decorated;

    public LoggingCommandHandlerWithResultDecorator(
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor,
        ICommandHandler<TCommand, TResult> decorated)
    {
        _logger = logger;
        _executionContextAccessor = executionContextAccessor;
        _decorated = decorated;
    }

    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        using (LogContext.Push(new RequestLogEnricher(_executionContextAccessor), new CommandLogEnricher(command)))
        {
            try
            {
                _logger.Information("Executing command {@Command}", command);
                var result = await _decorated.Handle(command, cancellationToken);
                _logger.Information("Command processed successful, result {Result}", result);
                return result;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Command processing failed");
                throw;
            }
        }
    }

    private class CommandLogEnricher : ILogEventEnricher
    {
        private readonly ICommand<TResult> _command;

        public CommandLogEnricher(ICommand<TResult> command)
        {
            _command = command;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) =>
            logEvent.AddOrUpdateProperty(new LogEventProperty("Context",
                new ScalarValue($"Command:{_command.Id.ToString()}")));
    }

    private class RequestLogEnricher : ILogEventEnricher
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public RequestLogEnricher(IExecutionContextAccessor executionContextAccessor)
        {
            _executionContextAccessor = executionContextAccessor;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_executionContextAccessor.IsAvailable)
                logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId",
                    new ScalarValue(_executionContextAccessor.CorrelationId)));
        }
    }
}