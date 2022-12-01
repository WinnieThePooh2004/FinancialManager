using Frontend.HttpService;
using Frontend.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddScoped<IFinancialOperationRequests, FinancialOperationRequests>();
builder.Services.AddScoped<IOperationTypesRequests, OperationTypesRequests>();

builder.Services.AddHttpClient("FMApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7203/");
});

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
