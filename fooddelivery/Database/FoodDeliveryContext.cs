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
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Suborder> Suborders { get; set; }
        public DbSet<TokenJWT> TokensJWT { get; set; }
        public DbSet<Guimarkz_commands> Commands { get; set; }

        //Contracts
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


            modelBuilder.Entity<AddressType>().HasData(
                    new AddressType { Id = 1, Name = "Casa" },
                    new AddressType { Id = 2, Name = "Trabalho" }
            );

            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Pratos" },
                    new Category { Id = 2, Name = "Sobremesas" },
                    new Category { Id = 3, Name = "Bebidas" }
            );

            modelBuilder.Entity<DeliveryStatus>().HasData(
                    new DeliveryStatus { Id = 1, Name = "Aberto" },
                    new DeliveryStatus { Id = 2, Name = "Em progresso" },
                    new DeliveryStatus { Id = 3, Name = "Pronto" },
                    new DeliveryStatus { Id = 4, Name = "Saiu para entrega" },
                    new DeliveryStatus { Id = 5, Name = "Entregue" },
                    new DeliveryStatus { Id = 6, Name = "Não entregue" },
                    new DeliveryStatus { Id = 7, Name = "Cancelado" }
            );

            modelBuilder.Entity<PaymentType>().HasData(
                    new PaymentType { Id = 1, Name = "Dinheiro" },
                    new PaymentType { Id = 2, Name = "Cartão" }
            );

            modelBuilder.Entity<Role>().HasData(
                    new Role { Id = 1, Name = "Administrator", NormalizedName = "Administrator" },
                    new Role { Id = 2, Name = "Common", NormalizedName = "Common" }
            );

        }
    }
}