﻿namespace ChangeMe.Shared.Application.Outbox;

public interface IOutbox
{
    void Add(OutboxMessage message);

    Task SaveAsync();
}