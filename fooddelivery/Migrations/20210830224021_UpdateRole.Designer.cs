﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fooddelivery.Database;

namespace fooddelivery.Migrations
{
    [DbContext(typeof(FoodDeliveryContext))]
    [Migration("20210830224021_UpdateRole")]
    partial class UpdateRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("fooddelivery.Models.Access.AccessKey", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("KeyType")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DataGerada")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Key", "Email", "KeyType");

                    b.ToTable("AccessKeys");
                });

            modelBuilder.Entity("fooddelivery.Models.Address", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Addon")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("AddressTypeId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Neighborhood")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Number")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Standard")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("State")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<double?>("X_coordinate")
                        .HasColumnType("double");

                    b.Property<double?>("Y_coordinate")
                        .HasColumnType("double");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("AddressTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("fooddelivery.Models.AddressType", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("AddressTypes");

                    b.HasData(
                        new
                        {
                            Id = 1ul,
                            Name = "Casa"
                        },
                        new
                        {
                            Id = 2ul,
                            Name = "Trabalho"
                        });
                });

            modelBuilder.Entity("fooddelivery.Models.Category", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1ul,
                            Name = "Pratos"
                        },
                        new
                        {
                            Id = 2ul,
                            Name = "Sobremesas"
                        },
                        new
                        {
                            Id = 3ul,
                            Name = "Bebidas"
                        });
                });

            modelBuilder.Entity("fooddelivery.Models.Change", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<ulong>("FoodId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("IngredientId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("SuborderId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("SuborderId");

                    b.HasIndex("FoodId", "IngredientId");

                    b.ToTable("Changes");
                });

            modelBuilder.Entity("fooddelivery.Models.Contact", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Facebook")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Instagram")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Tel")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Twitter")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Whatsapp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("fooddelivery.Models.Contracts.Additional", b =>
                {
                    b.Property<ulong>("FoodId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("IngredientId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("FoodId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("Additional");
                });

            modelBuilder.Entity("fooddelivery.Models.Contracts.Feedbacks", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("OrderId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Note")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("UserId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("fooddelivery.Models.Contracts.FoodIngredients", b =>
                {
                    b.Property<ulong>("FoodId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("IngredientId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("FoodId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("FoodIngredients");
                });

            modelBuilder.Entity("fooddelivery.Models.DeliveryStatus", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("DeliveryStatus");

                    b.HasData(
                        new
                        {
                            Id = 1ul,
                            Name = "Aberto"
                        },
                        new
                        {
                            Id = 2ul,
                            Name = "Em progresso"
                        },
                        new
                        {
                            Id = 3ul,
                            Name = "Pronto"
                        },
                        new
                        {
                            Id = 4ul,
                            Name = "Saiu para entrega"
                        },
                        new
                        {
                            Id = 5ul,
                            Name = "Entregue"
                        },
                        new
                        {
                            Id = 6ul,
                            Name = "Não entregue"
                        },
                        new
                        {
                            Id = 7ul,
                            Name = "Cancelado"
                        });
                });

            modelBuilder.Entity("fooddelivery.Models.Food", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<bool>("Available")
                        .HasColumnType("tinyint(1)");

                    b.Property<ulong>("CategoryId")
                        .HasColumnType("bigint unsigned");

                    b.Property<bool>("IsAppetizer")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("fooddelivery.Models.Image", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<byte[]>("Data")
                        .HasColumnType("longblob");

                    b.Property<ulong>("FoodId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Size")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("fooddelivery.Models.Ingredient", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Unity")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("fooddelivery.Models.Order", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("AddressId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("DeliveryStatusId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Note")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("PaymentTypeId")
                        .HasColumnType("bigint unsigned");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("ShoppingTime")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("DeliveryStatusId");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("fooddelivery.Models.PaymentType", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1ul,
                            Name = "Dinheiro"
                        },
                        new
                        {
                            Id = 2ul,
                            Name = "Cartão"
                        });
                });

            modelBuilder.Entity("fooddelivery.Models.Suborder", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("FoodId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Note")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("OrderId")
                        .HasColumnType("bigint unsigned");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("OrderId");

                    b.ToTable("Suborders");
                });

            modelBuilder.Entity("fooddelivery.Models.TokenJWT", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpirationRefreshToken")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpirationToken")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Used")
                        .HasColumnType("tinyint(1)");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TokensJWT");
                });

            modelBuilder.Entity("fooddelivery.Models.Users.Role", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1ul,
                            ConcurrencyStamp = "279b3c5e-8ca7-4951-80d9-fa82c4c9f777",
                            Name = "Administrator",
                            NormalizedName = "Administrator"
                        },
                        new
                        {
                            Id = 2ul,
                            ConcurrencyStamp = "d285f987-c063-45c7-b92b-40eafe0bdf43",
                            Name = "Common",
                            NormalizedName = "Common"
                        });
                });

            modelBuilder.Entity("fooddelivery.Models.Users.RoleClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("RoleId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("fooddelivery.Models.Users.User", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned")
                        .HasColumnName("Id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("fooddelivery.Models.Users.UserClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("fooddelivery.Models.Users.UserLogins", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("fooddelivery.Models.Users.UserRoles", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("RoleId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("fooddelivery.Models.Users.UserTokens", b =>
                {
                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("fooddelivery.Models.Address", b =>
                {
                    b.HasOne("fooddelivery.Models.AddressType", "AddressType")
                        .WithMany("Orders")
                        .HasForeignKey("AddressTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fooddelivery.Models.Users.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("fooddelivery.Models.Change", b =>
                {
                    b.HasOne("fooddelivery.Models.Suborder", "Suborder")
                        .WithMany("Changes")
                        .HasForeignKey("SuborderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fooddelivery.Models.Contracts.Additional", "Additional")
                        .WithMany("Changes")
                        .HasForeignKey("FoodId", "IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Additional");

                    b.Navigation("Suborder");
                });

            modelBuilder.Entity("fooddelivery.Models.Contact", b =>
                {
                    b.HasOne("fooddelivery.Models.Users.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("fooddelivery.Models.Contracts.Additional", b =>
                {
                    b.HasOne("fooddelivery.Models.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fooddelivery.Models.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("fooddelivery.Models.Contracts.Feedbacks", b =>
                {
                    b.HasOne("fooddelivery.Models.Order", "Order")
                        .WithMany("Feedbacks")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fooddelivery.Models.Users.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("fooddelivery.Models.Contracts.FoodIngredients", b =>
                {
                    b.HasOne("fooddelivery.Models.Food", "Food")
                        .WithMany("FoodIngredients")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fooddelivery.Models.Ingredient", "Ingredient")
                        .WithMany("FoodIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("fooddelivery.Models.Food", b =>
                {
                    b.HasOne("fooddelivery.Models.Category", "Category")
                        .WithMany("Foods")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("fooddelivery.Models.Image", b =>
                {
                    b.HasOne("fooddelivery.Models.Food", "Food")
                        .WithMany("Images")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("fooddelivery.Models.Order", b =>
                {
                    b.HasOne("fooddelivery.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fooddelivery.Models.DeliveryStatus", "DeliveryStatus")
                        .WithMany("Orders")
                        .HasForeignKey("DeliveryStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fooddelivery.Models.PaymentType", "PaymentType")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fooddelivery.Models.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("DeliveryStatus");

                    b.Navigation("PaymentType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("fooddelivery.Models.Suborder", b =>
                {
                    b.HasOne("fooddelivery.Models.Food", "Food")
                        .WithMany("Suborders")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fooddelivery.Models.Order", "Order")
                        .WithMany("Suborders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("fooddelivery.Models.TokenJWT", b =>
                {
                    b.HasOne("fooddelivery.Models.Users.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("fooddelivery.Models.Users.RoleClaims", b =>
                {
                    b.HasOne("fooddelivery.Models.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fooddelivery.Models.Users.UserClaims", b =>
                {
                    b.HasOne("fooddelivery.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fooddelivery.Models.Users.UserLogins", b =>
                {
                    b.HasOne("fooddelivery.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fooddelivery.Models.Users.UserRoles", b =>
                {
                    b.HasOne("fooddelivery.Models.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("fooddelivery.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fooddelivery.Models.Users.UserTokens", b =>
                {
                    b.HasOne("fooddelivery.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("fooddelivery.Models.AddressType", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("fooddelivery.Models.Category", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("fooddelivery.Models.Contracts.Additional", b =>
                {
                    b.Navigation("Changes");
                });

            modelBuilder.Entity("fooddelivery.Models.DeliveryStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("fooddelivery.Models.Food", b =>
                {
                    b.Navigation("FoodIngredients");

                    b.Navigation("Images");

                    b.Navigation("Suborders");
                });

            modelBuilder.Entity("fooddelivery.Models.Ingredient", b =>
                {
                    b.Navigation("FoodIngredients");
                });

            modelBuilder.Entity("fooddelivery.Models.Order", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Suborders");
                });

            modelBuilder.Entity("fooddelivery.Models.PaymentType", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("fooddelivery.Models.Suborder", b =>
                {
                    b.Navigation("Changes");
                });

            modelBuilder.Entity("fooddelivery.Models.Users.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Contacts");

                    b.Navigation("Feedbacks");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
