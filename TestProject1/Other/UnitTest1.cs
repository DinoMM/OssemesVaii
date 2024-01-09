using Bunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OSsemes.Areas.Identity.Data;
using OSsemes.Data.Database;

namespace Other
{
    /// <summary>
    /// Pre ostatnÈ odvedvia
    /// </summary>
    //public class UnitTest1 : TestContext
    //{
    //    private readonly DataContext _context;

    //    public UnitTest1()
    //    {
    //        SqlConnectionStringBuilder DBStringBuilder = new SqlConnectionStringBuilder()
    //        {
    //            DataSource = "sqlserver",
    //            UserID = "sa",
    //            Password = @"FaKePaSsWoRd???!!!@@@123456789",
    //            InitialCatalog = "HlavnaDatabaza",
    //            IntegratedSecurity = false,
    //            TrustServerCertificate = true
    //        };



    //        ///////////////////////Identity////////////////
    //        Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(DBStringBuilder.ConnectionString));

    //        Services.AddIdentity<IdentityUserOwn, IdentityRole>(opt =>
    //        {
    //            opt.Password.RequireDigit = true;
    //            opt.Password.RequiredLength = 6;
    //            opt.Password.RequireLowercase = true;
    //            opt.Password.RequireUppercase = true;
    //            opt.Password.RequireNonAlphanumeric = false;
    //            opt.SignIn.RequireConfirmedEmail = false;


    //        }).AddEntityFrameworkStores<DataContext>()
    //            .AddDefaultTokenProviders()
    //            .AddRoles<IdentityRole>();

    //        var serviceProvider = Services.BuildServiceProvider();
            
    //        _context = serviceProvider.GetService<DataContext>();
    //        Console.WriteLine( "dsasdasda\n");
    //    }



    //    #region Group 1
    //    /// <summary>
    //    /// 1.Pripojenie na server
    //    /// 2.Overenie, ûe sa d· pripojiù a ûe je vytvoren· datab·za
    //    /// </summary>
    //    [Fact]
    //    public void Test_Database1()
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    //        optionsBuilder.UseInMemoryDatabase("TestDb");
    //        var context = new DataContext(optionsBuilder.Options);

    //        Assert.NotNull(context);
    //        if (context == null)
    //        {
    //            return;
    //        }
    //        Assert.True(context.Database.CanConnect());
    //        Assert.True(context.Database.EnsureCreated());
    //    }
    //    /// <summary>
    //    /// 1.Pripojenie na server
    //    /// 2.
    //    /// </summary>
    //    [Fact]
    //    public void Test_Database2()
    //    {
    //        var optionsBuilderTest = new DbContextOptionsBuilder<DataContext>();
    //        optionsBuilderTest.UseInMemoryDatabase("TestDb");
    //        var contextTest = new DataContext(optionsBuilderTest.Options);

    //        Assert.NotNull(_context);

    //        var lista = _context.Roles.ToList();
    //        contextTest.Roles.AddRange(lista);         //pridanie vysledkov z realnej databazy
    //        Assert.True(contextTest.SaveChanges() > 0);


    //    }
    //    /// <summary>
    //    /// 1.Pripojenie na server
    //    /// 2.Overenie, ûe server obsahuje vöetky pouûivateæskÈ role bez duplik·tov
    //    /// </summary>
    //    [Fact]
    //    public void Test_Database3()
    //    {
    //        var optionsBuilderRealDb = new DbContextOptionsBuilder<DataContext>();
    //        var contextReal = new DataContext(optionsBuilderRealDb.Options);

    //        var optionsBuilderTest = new DbContextOptionsBuilder<DataContext>();
    //        optionsBuilderTest.UseInMemoryDatabase("TestDb");
    //        var contextTest = new DataContext(optionsBuilderTest.Options);

    //        contextTest.Roles.AddRange(contextReal.Roles.ToList());

    //        if (contextTest == null)
    //        {
    //            return;
    //        }

    //        List<string> list = new List<string>() { "Guest", "Reception", "Admin" };
    //        foreach (var role in contextTest.Roles)
    //        {
    //            Assert.NotNull(role);
    //            Assert.True(list.Remove(role.Name));
    //        }

    //        Assert.Equal(0, list.Count);
    //    }
    //    /// <summary>
    //    /// 1.Pripojenie na server
    //    /// 2.Overenie, ûe server obsahuje vöetky izby bez duplik·tov
    //    /// </summary>
    //    [Fact]
    //    public void Test_Database4()
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    //        optionsBuilder.UseInMemoryDatabase("TestDb");
    //        var context = new DataContext(optionsBuilder.Options);
    //        if (context == null)
    //        {
    //            return;
    //        }

    //        List<string> list = new List<string>() { "101", "102", "201", "202", "301", "302" };
    //        foreach (var role in context.HRooms)
    //        {
    //            Assert.NotNull(role);
    //            Assert.True(list.Remove(role.RoomNumber));
    //        }
    //        Assert.Equal(0, list.Count);
    //    }
    //    #endregion
    //}
}