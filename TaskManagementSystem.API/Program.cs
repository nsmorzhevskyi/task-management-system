using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskManagementSystem.API;
using TaskManagementSystem.API.Configurations;
using TaskManagementSystem.Business.Commands;
using TaskManagementSystem.Business.Events;
using TaskManagementSystem.Data;
using TaskManagementSystem.Data.Repositories;
using TaskManagementSystem.Infrastructure.Configuration;
using TaskManagementSystem.Infrastructure.Implementations;
using TaskManagementSystem.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<TaskManagementSystemContext>(options =>
    options.UseSqlServer(config.GetConnectionString(TaskManagementSystemConfiguration.ConnectionString)));

builder.Services.Configure<MessageBrokerSettings>(config.GetSection("MessageBroker"));

builder.Services.AddSingleton(sp => 
    sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<TaskUpdatedEventConsumer>(cfg => cfg.UseMessageRetry(r => r.Interval(3, 500)));
    
    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.UserName);
            h.Password(settings.Password);
        });
    });
});

builder.Services.AddSingleton<ExceptionHandlerMiddleware>();

builder.Services.AddTransient<IEventBus, EventBus>();

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<CreateTaskCommand>(); });

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();