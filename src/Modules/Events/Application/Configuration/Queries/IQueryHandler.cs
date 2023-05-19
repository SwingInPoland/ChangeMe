using ChangeMe.Modules.Events.Application.Contracts;
using MediatR;

namespace ChangeMe.Modules.Events.Application.Configuration.Queries;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult> { }