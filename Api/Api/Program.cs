using Api.IRepository;
using Api.IService;
using Api.Models;
using Api.Repository;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SoccerContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SoccerContext") ?? throw new InvalidOperationException("Connect String AppDbContext is not found"));
});

builder.Services.AddTransient<IStatusRepository, StatusRepository>();
builder.Services.AddTransient<ICustomerAuthenticationService, CustomerAuthenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
