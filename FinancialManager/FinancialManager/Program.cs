using Microsoft.EntityFrameworkCore;
using FinancialManager.Data;
using FinancialManager.Models;
using FinancialManager.Services.CRUDServices;
using FinancialManager.Services.ReportService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IFinancialManagerContext, FinancialManagerContext>();
builder.Services.AddDbContext<FinancialManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FinancialManagerContext") ?? throw new InvalidOperationException("Connection string 'FinancialManagerContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IService<FinancialOperation>, FinancialOperationService>();
builder.Services.AddScoped<IService<OperationType>, OperationTypeService>();
builder.Services.AddScoped<IReportService, ReportService>();

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
