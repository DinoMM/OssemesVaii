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
using Blazored.SessionStorage;
using Microsoft.Extensions.Configuration;
using OSsemes.Pages.LoggedIn;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



///////////////////SQL SERVER CONNECTION/////////////////////////////
SqlConnectionStringBuilder DBStringBuilder = new SqlConnectionStringBuilder();  //(pomohol som si z internetu tutoriály/AI)
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
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(DBStringBuilder.ConnectionString));  //(pomohol som si z internetu tutoriály/AI)

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
    .AddRoles<IdentityRole>();      //(pomohol som si z internetu tutoriály/AI)



////////////////////////////////////////////////
builder.Services.AddScoped<Room>();
builder.Services.AddScoped<Rezervation>();
builder.Services.AddBlazoredSessionStorage();           //pre ukladanie dovtedy ked sa nezatvori prehliadac
                                                        //najst nuget alebo uz funguje?                       //Blazored.LocalStorage  pre ukladanie aj po zavreti prehliadaca
                                                        ////////////////////////////////////////////////


var app = builder.Build();

////pridavanie potrebnych zánamov do db ak sa nenájdu
using (var scope = app.Services.CreateScope())      //vytvorenie zakladnych uctov
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();    //(pomohol som si z internetu tutoriály/AI)
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUserOwn>>(); //(pomohol som si z internetu tutoriály/AI)
    if (dbContext is not null && userManager is not null)
    {
        RolesData.SeedRoles(app.Services).Wait();       //pridanie roli do systemu

        if (dbContext.HRooms.Count() == 0)        //prida zaznamy ak je tabulka prazdna
        {
            dbContext.HRooms.Add(new Room() { RoomNumber = "101", RoomCategory = "Izba Deluxe", MaxNumberOfGuest = 3, Cost = 100.00 });
            dbContext.HRooms.Add(new Room() { RoomNumber = "102", RoomCategory = "Izba Deluxe", MaxNumberOfGuest = 3, Cost = 100.00 });
            dbContext.HRooms.Add(new Room() { RoomNumber = "201", RoomCategory = "Izba Apartman", MaxNumberOfGuest = 3, Cost = 150.00 });
            dbContext.HRooms.Add(new Room() { RoomNumber = "202", RoomCategory = "Izba Apartman", MaxNumberOfGuest = 3, Cost = 150.00 });
            dbContext.HRooms.Add(new Room() { RoomNumber = "301", RoomCategory = "Izba ApaDelux", MaxNumberOfGuest = 6, Cost = 210.00 });
            dbContext.HRooms.Add(new Room() { RoomNumber = "302", RoomCategory = "Izba ApaDelux", MaxNumberOfGuest = 6, Cost = 210.00 });
        }

        if (dbContext.Users.FirstOrDefault(x => x.UserName == "admin@gmail.com") is null)
        {
            var identity = new IdentityUserOwn { UserName = "admin@gmail.com", Name = "Admin", Surname = "Admin", Email = "admin@gmail.com" };
            var result = await userManager.CreateAsync(identity, "Heslo123");
            await userManager.AddToRoleAsync(identity, "Admin");
            if (!result.Succeeded)
            {
                Console.WriteLine("Chyba pri vytvarani admina!");
            }
        }
        if (dbContext.Users.FirstOrDefault(x => x.UserName == "reception@gmail.com") is null)
        {
            var identity = new IdentityUserOwn { UserName = "reception@gmail.com", Name = "Reception", Surname = "Reception", Email = "reception@gmail.com" };
            var result = await userManager.CreateAsync(identity, "Heslo123");
            await userManager.AddToRoleAsync(identity, "Reception");
            if (!result.Succeeded)
            {
                Console.WriteLine("Chyba pri vytvarani recepcneho!");
            }
        }
        if (dbContext.Users.FirstOrDefault(x => x.UserName == "guest@gmail.com") is null)
        {
            var identity = new IdentityUserOwn { UserName = "guest@gmail.com", Name = "Guest", Surname = "Guest", Email = "guest@gmail.com" };
            var result = await userManager.CreateAsync(identity, "Heslo123");
            await userManager.AddToRoleAsync(identity, "Guest");
            if (!result.Succeeded)
            {
                Console.WriteLine("Chyba pri vytvarani hosta!");
            }
        }
        await dbContext.SaveChangesAsync();
    }

}
//////////////////////////

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions        //(pomohol som si z internetu tutoriály/AI)
{
    FileProvider = new PhysicalFileProvider(            //vlastna cesta k media suborom
     Path.Combine(Directory.GetCurrentDirectory(), "Data/Media")),
    RequestPath = "/Media"
});

app.UseRouting();

app.UseAuthentication();        //povolenie autentifikacie a autorizacie
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
