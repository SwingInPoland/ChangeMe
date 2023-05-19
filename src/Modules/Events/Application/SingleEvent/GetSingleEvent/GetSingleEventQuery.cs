using ChangeMe.Modules.Events.Application.Configuration.Queries;

namespace ChangeMe.Modules.Events.Application.SingleEvent.GetSingleEvent;

public record GetSingleEventQuery(Guid SingleEventId) : QueryBase<SingleEventDto>;