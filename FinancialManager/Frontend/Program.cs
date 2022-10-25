using FinancialManager.Data;
using FinancialManager.Models;
using FinancialManager.Services.CRUDServices;
using FinancialManager.Services.ReportService;
using Frontend.AppHttpClient;
using Frontend.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddSingleton<AppHttpClient>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
