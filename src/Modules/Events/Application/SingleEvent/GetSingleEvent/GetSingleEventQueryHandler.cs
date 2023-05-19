using ChangeMe.Modules.Events.Application.Configuration.Queries;
using ChangeMe.Shared.Application.Data;

namespace ChangeMe.Modules.Events.Application.SingleEvent.GetSingleEvent;

internal class GetSingleEventQueryHandler : IQueryHandler<GetSingleEventQuery, SingleEventDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetSingleEventQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public Task<SingleEventDto> Handle(GetSingleEventQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        //TODO: Implement this and create rest queries
        throw new NotImplementedException();
        // return await connection.QuerySingleAsync<MeetingDetailsDto>(
        //     "SELECT " +
        //     $"[MeetingDetails].[Id] AS [{nameof(MeetingDetailsDto.Id)}], " +
        //     $"[MeetingDetails].[MeetingGroupId] AS [{nameof(MeetingDetailsDto.MeetingGroupId)}], " +
        //     $"[MeetingDetails].[Title] AS [{nameof(MeetingDetailsDto.Title)}], " +
        //     $"[MeetingDetails].[TermStartDate] AS [{nameof(MeetingDetailsDto.TermStartDate)}], " +
        //     $"[MeetingDetails].[TermEndDate] AS [{nameof(MeetingDetailsDto.TermEndDate)}], " +
        //     $"[MeetingDetails].[Description] AS [{nameof(MeetingDetailsDto.Description)}], " +
        //     $"[MeetingDetails].[LocationName] AS [{nameof(MeetingDetailsDto.LocationName)}], " +
        //     $"[MeetingDetails].[LocationAddress] AS [{nameof(MeetingDetailsDto.LocationAddress)}], " +
        //     $"[MeetingDetails].[LocationPostalCode] AS [{nameof(MeetingDetailsDto.LocationPostalCode)}], " +
        //     $"[MeetingDetails].[LocationCity] AS [{nameof(MeetingDetailsDto.LocationCity)}], " +
        //     $"[MeetingDetails].[AttendeesLimit] AS [{nameof(MeetingDetailsDto.AttendeesLimit)}], " +
        //     $"[MeetingDetails].[GuestsLimit] AS [{nameof(MeetingDetailsDto.GuestsLimit)}], " +
        //     $"[MeetingDetails].[RSVPTermStartDate] AS [{nameof(MeetingDetailsDto.RSVPTermStartDate)}], " +
        //     $"[MeetingDetails].[RSVPTermEndDate] AS [{nameof(MeetingDetailsDto.RSVPTermEndDate)}], " +
        //     $"[MeetingDetails].[EventFeeValue] AS [{nameof(MeetingDetailsDto.EventFeeValue)}], " +
        //     $"[MeetingDetails].[EventFeeCurrency] AS [{nameof(MeetingDetailsDto.EventFeeCurrency)}] " +
        //     "FROM [meetings].[v_MeetingDetails] AS [MeetingDetails] " +
        //     "WHERE [MeetingDetails].[Id] = @MeetingId",
        //     new
        //     {
        //         query.MeetingId
        //     });
    }
}