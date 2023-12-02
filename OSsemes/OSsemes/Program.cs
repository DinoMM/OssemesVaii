using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OSsemes.Data;
using OSsemes.Data.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using OSsemes.Areas.Identity.Data;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



///////////////////SQL SERVER CONNECTION/////////////////////////////
SqlConnectionStringBuilder DBStringBuilder = new SqlConnectionStringBuilder();
;
DBStringBuilder.DataSource = Environment.GetEnvironmentVariable("DB_H");
DBStringBuilder.UserID = "sa";
DBStringBuilder.Password = Environment.GetEnvironmentVariable("DB_P");
DBStringBuilder.InitialCatalog = Environment.GetEnvironmentVariable("DB_N");
DBStringBuilder.IntegratedSecurity = false;
DBStringBuilder.TrustServerCertificate = true;
/////////////////////OTHER//////////////////////

//builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(DBStringBuilder.ConnectionString));

///////////////////////Identity////////////////
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(DBStringBuilder.ConnectionString));

builder.Services.AddIdentity<IdentityUserOwn, IdentityRole>(opt =>
    {
        opt.Password.RequireDigit = true;
        opt.Password.RequiredLength = 6;
        opt.Password.RequireLowercase = true;
        opt.Password.RequireUppercase = true;
        opt.Password.RequireNonAlphanumeric = false;
        opt.SignIn.RequireConfirmedEmail = false;


    }).AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>();



////////////////////////////////////////////////
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(            //vlastna cesta k media suborom
     Path.Combine(Directory.GetCurrentDirectory(), "Data/Media")),
    RequestPath = "/Media"
});

app.UseRouting();

app.UseAuthentication();        //povolenie autentifikacie a autorizacie
app.UseAuthorization();

RolesData.SeedRoles(app.Services).Wait();       //pridanie roli do systemu

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
