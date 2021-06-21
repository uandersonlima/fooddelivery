using fooddelivery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using fooddelivery.Models.Contracts;
using fooddelivery.Models.Access;
using Microsoft.AspNetCore.Identity;
using fooddelivery.Models.Users;

namespace fooddelivery.Database
{
    public class FoodDeliveryContext : IdentityDbContext<User, Role, ulong, UserClaims, UserRoles, UserLogins,
        RoleClaims, UserTokens>
    {
        public FoodDeliveryContext(DbContextOptions<FoodDeliveryContext> options) : base(options) { }



        public DbSet<AccessKey> AccessKeys { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<DeliveryStatus> DeliveryStatus { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Suborder> Suborders { get; set; }

        //Contracts
        public DbSet<Additional> Additional { get; set; }
        public DbSet<Feedbacks> Feedbacks { get; set; }
        public DbSet<FoodIngredients> FoodIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                        .ToTable("Users")
                        .Property(p => p.Id)
                        .HasColumnName("Id");
            modelBuilder.Entity<UserClaims>().ToTable("Claims");
            modelBuilder.Entity<UserLogins>().ToTable("Logins");
            modelBuilder.Entity<UserTokens>().ToTable("Tokens");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<RoleClaims>().ToTable("RoleClaims");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");

            modelBuilder.Entity<AccessKey>().HasKey(pk =>
                    new { pk.Key, pk.Email, pk.KeyType });
            modelBuilder.Entity<Additional>().HasKey(pk =>
                    new { pk.FoodId, pk.IngredientId });
            modelBuilder.Entity<Feedbacks>().HasKey(pk =>
                    new { pk.UserId, pk.OrderId });
            modelBuilder.Entity<FoodIngredients>().HasKey(pk =>
                    new { pk.FoodId, pk.IngredientId });
        }
    }
}