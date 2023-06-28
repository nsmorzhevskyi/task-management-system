using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Configurations;
using TaskManagementSystem.Business.Commands;
using TaskManagementSystem.Data;
using TaskManagementSystem.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<TaskManagementSystemContext>(options =>
    options.UseSqlServer(config.GetConnectionString(TaskManagementSystemConfiguration.ConnectionString)));

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<CreateTaskCommand>(); });

builder.Services.AddScoped<ITaskRepository, TaskRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();