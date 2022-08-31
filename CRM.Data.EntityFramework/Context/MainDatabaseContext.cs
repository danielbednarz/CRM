using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRM.EntityFramework.Context
{
    public class MainDatabaseContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, 
        AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(x => x.UserRoles)
                .WithOne(y => y.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
                .HasMany(x => x.UserRoles)
                .WithOne(y => y.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();
        }
    }
}
