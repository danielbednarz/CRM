using CRM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.EntityFramework.Context
{
    public class MainDatabaseContext : DbContext
    {
        public MainDatabaseContext(DbContextOptions options) : base(options)
        {

        }
        //Add-Migration -Context MainDatabaseContext -o Migrations/MainDatabaseMigrations <Nazwa migracji>
        //Update-Database -Context MainDatabaseContext
        //Remove-Migration -Context MainDatabaseContext
        // dotnet ef migrations add Init -o Data\Migrations
        // dotnet ef database update

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

        }
    }
}
