using fooddelivery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using fooddelivery.Models.Contracts;

namespace fooddelivery.Database
{
    public class FoodDeliveryContext : IdentityDbContext<User>
    {
        public FoodDeliveryContext(DbContextOptions<FoodDeliveryContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
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

            modelBuilder.Entity<Additional>().HasKey(pk =>
                    new { pk.FoodCode, pk.IngredientCode });
            modelBuilder.Entity<Feedbacks>().HasKey(pk =>
                    new { pk.UserId, pk.OrderCode });
            modelBuilder.Entity<FoodIngredients>().HasKey(pk =>
                    new { pk.FoodCode, pk.IngredientCode });
        }
    }
}