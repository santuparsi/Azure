using HandsOnEFCodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
namespace HandsOnEFCodeFirst.Entities
{
    public class UworldDBContext:DbContext
    {
        //Entity set
        public DbSet<Product> Products { get; set; }
        public DbSet<Movie> Movies { get; set; }
        //configure connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"server=DESKTOP-4O1D65I\SQLEXPRESS;database=UworldDB;trusted_connection=true");
            optionsBuilder.UseSqlServer(@"Server=tcp:myserver57.database.windows.net,1433;Initial Catalog=mysqldb427;Persist Security Info=False;User ID=azureuser;Password=@zureuser123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

    }
}
