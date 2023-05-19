using Quartz;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.Inbox;

[DisallowConcurrentExecution]
public class ProcessInboxJob : IJob
{
    public async Task Execute(IJobExecutionContext context) =>
        await CommandsExecutor.ExecuteAsync(new ProcessInboxCommand());
}