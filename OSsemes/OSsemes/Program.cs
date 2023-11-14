using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OSsemes.Data;
using OSsemes.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

///////////////////SQL SERVER CONNECTION/////////////////////////////
SqlConnectionStringBuilder DBStringBuilder = new SqlConnectionStringBuilder();

DBStringBuilder.DataSource = Environment.GetEnvironmentVariable("DB_H");
DBStringBuilder.UserID = "sa";
DBStringBuilder.Password = Environment.GetEnvironmentVariable("DB_P");
DBStringBuilder.InitialCatalog = Environment.GetEnvironmentVariable("DB_N");
DBStringBuilder.IntegratedSecurity = false;
DBStringBuilder.TrustServerCertificate = true;
builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(DBStringBuilder.ConnectionString));
////////////////////////////////////////////////

builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
