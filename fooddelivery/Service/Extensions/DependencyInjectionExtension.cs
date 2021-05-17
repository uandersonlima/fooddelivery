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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Auth.AspNetCore3;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using fooddelivery.Models.Helpers;
using fooddelivery.Models.DTO;

namespace fooddelivery.Service.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection svc, IConfiguration conf)
        {

            #region AutoMapper-Config
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DTOMapperProfile());
            });
            IMapper mapper = config.CreateMapper();
            svc.AddSingleton(mapper);
            #endregion


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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "fooddelivery", Version = "v1" });
            });

            svc.AddHttpContextAccessor();
            svc.AddSignalR();

            svc.Configure<EmailSettings>(conf.GetSection(EmailSettings.Position));
            svc.Configure<JWTSettings>(conf.GetSection(JWTSettings.Position));

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
                options.User.RequireUniqueEmail = true;
            });

            //Banco de Dados
            svc.AddDbContext<FoodDeliveryContext>(options => options.UseMySql(conf.GetConnectionString("FoodDeliveryContext"), new MySqlServerVersion(new Version(8, 0, 20))));


            svc.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                var JWTBearer = conf.GetSection("Auth:JWTBearer");
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTBearer["SecretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            // svc
            //      .AddAuthentication(o =>
            //     {
            //         // This forces challenge results to be handled by Google OpenID Handler, so there's no
            //         // need to add an AccountController that emits challenges for Login.
            //         //o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            //         // This forces forbid results to be handled by Google OpenID Handler, which checks if
            //         // extra scopes are required and does automatic incremental auth.
            //         //o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            //         // Default scheme that will handle everything else.
            //         // Once a user is authenticated, the OAuth2 token info is stored in cookies.
            //         o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //      }).AddJwtBearer(cfg =>
            //        {
            //            cfg.RequireHttpsMetadata = false;
            //            cfg.SaveToken = true;

            //            cfg.TokenValidationParameters = new TokenValidationParameters()
            //            {
            //                ValidateIssuerSigningKey = true,
            //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["AppSettings:JwtSecret"])),
            //                ValidateIssuer = false,
            //                ValidateAudience = false
            //            };
            //        });
            // .AddGoogleOpenIdConnect(googleOptions =>
            // {
            //     IConfigurationSection googleAuthNSection = conf.GetSection("Auth:GoogleOAuth2");
            //     googleOptions.ClientId = googleAuthNSection["ClientId"];
            //     googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
            // });




            // svc.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(cfg =>
            //        {
            //            cfg.RequireHttpsMetadata = false;
            //            cfg.SaveToken = true;

            //            cfg.TokenValidationParameters = new TokenValidationParameters()
            //            {
            //                ValidateIssuerSigningKey = true,
            //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["AppSettings:JwtSecret"])),
            //                ValidateIssuer = false,
            //                ValidateAudience = false
            //            };
            //        });
            // .AddJwtBearer(options =>
            //     {
            //         options.Authority = "https://securetoken.google.com/<ID DO SEU PROJETO>";
            //         options.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuer = true,
            //             ValidIssuer = "https://securetoken.google.com/",
            //             ValidateAudience = true,
            //             ValidAudience = "<ID DO SEU PROJETO>",
            //             ValidateLifetime = true
            //         };
            //     })
            //     .AddMicrosoftAccount(microsoftOptions =>
            //     {
            //         IConfigurationSection microsoftAuthNSection = conf.GetSection("Auth:MicrosoftOAuth2");
            //         microsoftOptions.ClientId = microsoftAuthNSection["ClientId"];
            //         microsoftOptions.ClientSecret = microsoftAuthNSection["ClientSecret"];
            //     })
            //     .AddGoogle(googleOptions =>
            //     {
            //         IConfigurationSection googleAuthNSection = conf.GetSection("Auth:GoogleOAuth2");
            //         googleOptions.ClientId = googleAuthNSection["ClientId"];
            //         googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
            //     });

            // .AddTwitter(twitterOptions =>
            // {
            //     IConfigurationSection twitterAuthNSection = conf.GetSection("Auth:TwitterOAuth2");
            //     twitterOptions.ConsumerKey = twitterAuthNSection["ConsumerAPIKey"];
            //     twitterOptions.ConsumerSecret = twitterAuthNSection["ConsumerSecret"];
            //     twitterOptions.RetrieveUserDetails = true;
            // })
            // .AddFacebook(facebookOptions =>
            // {
            //     IConfigurationSection facebookAuthNSection = conf.GetSection("Auth:FacebookOAuth2");
            //     facebookOptions.AppId = facebookAuthNSection["AppId"];
            //     facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
            // });


            svc.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                                        .AddEntityFrameworkStores<FoodDeliveryContext>()
                                        .AddDefaultTokenProviders();

            
            svc.AddScoped<SeedData>();

            //Services
            svc.AddScoped<IAuthService, AuthService>();
            svc.AddScoped<IAddressService, AddressService>();
            svc.AddScoped<ICategoryService, CategoryService>();
            svc.AddScoped<IChangeService, ChangeService>();
            svc.AddScoped<IContactService, ContactService>();
            svc.AddScoped<IDeliveryStatusService, DeliveryStatusService>();
            svc.AddScoped<IFoodService, FoodService>();
            svc.AddScoped<IImageService, ImageService>();
            svc.AddScoped<IIngredientService, IngredientService>();
            svc.AddScoped<IOrderService, OrderService>();
            svc.AddScoped<ISuborderService, SuborderService>();
            svc.AddScoped<IUserService, UserService>();
            //Repositories
            svc.AddScoped<IAddressRepository, AddressRepository>();
            svc.AddScoped<ICategoryRepository, CategoryRepository>();
            svc.AddScoped<IChangeRepository, ChangeRepository>();
            svc.AddScoped<IContactRepository, ContactRepository>();
            svc.AddScoped<IDeliveryStatusRepository, DeliveryStatusRepository>();
            svc.AddScoped<IFoodRepository, FoodRepository>();
            svc.AddScoped<IImageRepository, ImageRepository>();
            svc.AddScoped<IIngredientRepository, IngredientRepository>();
            svc.AddScoped<IOrderRepository, OrderRepository>();
            svc.AddScoped<ISuborderRepository, SuborderRepository>();
            svc.AddScoped<IUserRepository, UserRepository>();
            //Cors Policy
            svc.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

        }
    }
}