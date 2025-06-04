using Microsoft.EntityFrameworkCore;

namespace HandsOnMVCEFCoreCodeFirst_Demo2.Entities
{
    public class ZuciDB324Context:DbContext
    {
        //Define the entityset
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //define connectionstring to ZuciDB324
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:mysqldb427.database.windows.net,1433;Initial Catalog=PracticeDB;Persist Security Info=False;User ID=myadmin427;Password=427@dmin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;;",
                builder=>builder.EnableRetryOnFailure());
        }
    }
}
