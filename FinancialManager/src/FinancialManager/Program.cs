using Microsoft.EntityFrameworkCore;
using FinancialManager.Shared.Models;
using FinancialManager.Shared.DTOs;
using FinancialManager.Infrastructure.Repositiories;
using FinancialManager.MiddlewareFilters;
using FinancialManager.Shared.Interfaces.Repositiories;
using FinancialManager.Shared.Interfaces.Services;
using FinancialManager.Infrastructure;
using FinancialManager.Domain.Services;
using FluentValidation.AspNetCore;
using System.Reflection;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FinancialManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FinancialManagerContext") 
        ?? throw new InvalidOperationException("Connection string 'FinancialManagerContext' not found.")));

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<MiddlewareExceptionFilter>();
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.Load("FinancialManager.Domain"));

builder.Services.AddAutoMapper(Assembly.Load("FinancialManager.Domain"));

builder.Services.AddScoped<ICRUDService<FinancialOperationDTO>, FinancialOperationService>();
builder.Services.AddScoped<ICRUDService<OperationTypeDTO>, OperationTypeService>();
builder.Services.AddScoped<ICRUDRepository<FinancialOperation>, FinancialOperationRepository>();
builder.Services.AddScoped<ICRUDRepository<OperationType>, OperationTypeRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

public partial class Program
{

}