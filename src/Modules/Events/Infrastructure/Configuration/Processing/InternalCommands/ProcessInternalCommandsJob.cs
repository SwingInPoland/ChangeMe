﻿using Quartz;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.InternalCommands;

[DisallowConcurrentExecution]
public class ProcessInternalCommandsJob : IJob
{
    public async Task Execute(IJobExecutionContext context) =>
        await CommandsExecutor.ExecuteAsync(new ProcessInternalCommandsCommand());
}