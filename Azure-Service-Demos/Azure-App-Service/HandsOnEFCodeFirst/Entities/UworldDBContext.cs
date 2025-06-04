using HandsOnEFCodeFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
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
            //optionsBuilder.UseSqlServer(@"Server=tcp:myserver57.database.windows.net,1433;Initial Catalog=mysqldb427;Persist Security Info=False;User ID=azureuser;Password=@zureuser123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //optionsBuilder.UseSqlServer(@"Server=tcp:myserver67.database.windows.net,1433;Initial Catalog=Moviedb;Persist Security Info=False;User ID=myadmin;Password=Admin123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            const string secretName = "sqlconn1";
            var kvUri = $"https://mykey267.vault.azure.net/";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            var secret = client.GetSecretAsync(secretName);
            var sqlconnection = secret.Result.Value.Value;
            optionsBuilder.UseSqlServer(sqlconnection);

        }

    }
}
