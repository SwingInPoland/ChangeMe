﻿using Quartz;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.Outbox;

[DisallowConcurrentExecution]
public class ProcessOutboxJob : IJob
{
    public async Task Execute(IJobExecutionContext context) =>
        await CommandsExecutor.ExecuteAsync(new ProcessOutboxCommand());
}