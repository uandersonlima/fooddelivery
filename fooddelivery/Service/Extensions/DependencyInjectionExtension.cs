using System;
using fooddelivery.Database;
using fooddelivery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Mvc;
using fooddelivery.Service.Interfaces;
using fooddelivery.Service.Services;
using fooddelivery.Repository.Interfaces;
using fooddelivery.Repository.Repositories;

namespace fooddelivery.Service.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection svc, IConfiguration conf)
        {
            svc.Configure<ApiBehaviorOptions>(op =>
            {
                op.SuppressModelStateInvalidFilter = true;
            });

            svc.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            svc.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "fooddelivery", Version = "v1" });
            });

            svc.AddHttpContextAccessor();
            svc.AddSignalR();

            svc.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            //Banco de Dados
            svc.AddDbContext<FoodDeliveryContext>(options => options.UseSqlite(conf.GetConnectionString("FoodDeliveryContext")));

            //Authentication
            svc.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<FoodDeliveryContext>().AddDefaultTokenProviders();
            
           /* svc.AddAuthentication()
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = conf["Authentication:Microsoft:ClientId"];
                    microsoftOptions.ClientSecret = conf["Authentication:Microsoft:ClientSecret"];
                })
                .AddGoogle(googleOptions =>
                {
                    IConfigurationSection googleAuthNSection =
                    conf.GetSection("Authentication:Google");
                    googleOptions.ClientId = googleAuthNSection["ClientId"];
                    googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
                })
                .AddTwitter(twitterOptions =>
                {
                    twitterOptions.ConsumerKey = conf["Authentication:Twitter:ConsumerAPIKey"];
                    twitterOptions.ConsumerSecret = conf["Authentication:Twitter:ConsumerSecret"];
                    twitterOptions.RetrieveUserDetails = true;
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = conf["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = conf["Authentication:Facebook:AppSecret"];
                });
            */

            //Services
            svc.AddScoped<IAddressService, AddressService>();
            svc.AddScoped<ICategoryService, CategoryService>();
            svc.AddScoped<IChangeService, ChangeService>();
            svc.AddScoped<IContactService, ContactService>();
            svc.AddScoped<IFoodService, FoodService>();
            svc.AddScoped<IImageService, ImageService>();
            svc.AddScoped<IIngredientService, IngredientService>();
            svc.AddScoped<IOrderService, OrderService>();
            svc.AddScoped<ISuborderService, SuborderService>();
            //Repositories
            svc.AddScoped<IAddressRepository, AddressRepository>();
            svc.AddScoped<ICategoryRepository, CategoryRepository>();
            svc.AddScoped<IChangeRepository, ChangeRepository>();
            svc.AddScoped<IContactRepository, ContactRepository>();
            svc.AddScoped<IFoodRepository, FoodRepository>();
            svc.AddScoped<IImageRepository, ImageRepository>();
            svc.AddScoped<IIngredientRepository, IngredientRepository>();
            svc.AddScoped<IOrderRepository, OrderRepository>();
            svc.AddScoped<ISuborderRepository, SuborderRepository>();
            //Cors Policy
            svc.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyOrigin();
                    });
            });

        }
    }
}