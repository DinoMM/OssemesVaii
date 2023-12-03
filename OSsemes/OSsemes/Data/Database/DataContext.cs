using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OSsemes.Areas.Identity.Data;

namespace OSsemes.Data.Database
{
    public class DataContext : IdentityDbContext<IdentityUserOwn>
    {
        public DataContext(DbContextOptions<DataContext> opt): base(opt) 
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();        //vytvori databazu ak neexituje
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();   //vytvori tabulky ak databaza nema tabulky
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<Rezervation> Rezervations { get; set; }
    }

    public class YourDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Data Source=localhost;User ID=sa;Password=FaKePaSsWoRd???!!!@@@123456789;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Initial Catalog=HlavnaDatabaza");

            return new DataContext(optionsBuilder.Options);
        }
    }

   
}
