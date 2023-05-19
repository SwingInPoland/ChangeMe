using Autofac;
using Autofac.Extensions.DependencyInjection;
using ChangeMe.API.Configuration.Authorization;
using ChangeMe.API.Configuration.ExecutionContext;
using ChangeMe.API.Configuration.Extensions;
using ChangeMe.API.Configuration.Validation;
using ChangeMe.API.Modules.Events;
using ChangeMe.Modules.Events.Infrastructure.Configuration;
using ChangeMe.Shared.Application;
using ChangeMe.Shared.Domain;
using Hellang.Middleware.ProblemDetails;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

#region Autofac

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new EventsAutofacModule());
});

#endregion

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var loggerForApi = logger.ForContext("Module", "API");
loggerForApi.Information("Logger configured");

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables("Events_")
    .Build();

loggerForApi.Information("Connection string:" + configuration["EventsConnectionString"]);

AuthorizationChecker.CheckAllEndpoints();

builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();

//TODO: Add authentication

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

builder.Services.AddProblemDetails(x =>
{
    x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
    x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
});

var app = builder.Build();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

var container = app.Services.GetAutofacRoot();
var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

EventsStartup.Initialize(
    configuration["EventsConnectionString"],
    executionContextAccessor,
    logger,
    null);

app.UseMiddleware<CorrelationMiddleware>();

app.UseSwaggerDocumentation();

if (builder.Environment.IsDevelopment())
    app.UseProblemDetails();
else
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

app.UseHttpsRedirection();

app.UseRouting();

//TODO: needed?
//app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();