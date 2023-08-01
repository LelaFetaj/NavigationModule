using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NavigationModule.Authentication.Models.Entities.Roles;
using NavigationModule.Authentication.Models.Entities.Users;

namespace NavigationModule.Authentication.Brokers.Storages
{
    sealed partial class StorageBroker
    {
        private static void ConfigureIdentityTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable(name: "Roles");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable(name: "RoleClaims");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable(name: "UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable(name: "UserLogins");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable(name: "UserRoles");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable(name: "UserTokens");

            modelBuilder.Entity<User>(action =>
            {
                action.ToTable(name: "Users");

                action
                    .Property(prop => prop.Gender)
                    .HasConversion(
                        x => x.ToString(),
                        x => (Gender)Enum.Parse(typeof(Gender), x));

                action
                    .HasIndex(prop => prop.Email)
                    .IsUnique();

                action
                    .HasIndex(prop => prop.UserName)
                    .IsUnique();

                action.HasIndex(prop => prop.FirstName);
                action.HasIndex(prop => prop.LastName);

                action
                    .HasIndex(prop => prop.BirthDate)
                    .IsDescending();
            });
        }
    }
}