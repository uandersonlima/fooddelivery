using System;
using fooddelivery.Database;
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
using AutoMapper;
using fooddelivery.Models.Helpers;
using fooddelivery.Models.DTO;
using fooddelivery.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using fooddelivery.Authorization.Requirement;
using fooddelivery.Authorization.Handler;
using fooddelivery.Models.Users;
using System.Threading.Tasks;

namespace fooddelivery.Service.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection svc, IConfiguration conf)
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

            //Banco de Dados Mysql AWS
            svc.AddDbContext<FoodDeliveryContext>(options => options.UseMySql(conf.GetConnectionString("AWSMySql"), new MySqlServerVersion(new Version(8, 0, 20))));

            //Banco de dados Mysql Local 
            //svc.AddDbContext<FoodDeliveryContext>(options => options.UseMySql(conf.GetConnectionString("LocalMySql"), new MySqlServerVersion(new Version(8, 0, 20))));

            svc.AddDefaultIdentity<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<FoodDeliveryContext>()
                .AddDefaultTokenProviders();

            //Services
            svc.AddScoped<IAuthService, AuthService>();
            svc.AddScoped<IAddressService, AddressService>();
            svc.AddScoped<IAddressTypeService, AddressTypeService>();
            svc.AddScoped<ICategoryService, CategoryService>();
            svc.AddScoped<IChangeService, ChangeService>();
            svc.AddScoped<IContactService, ContactService>();
            svc.AddScoped<IDeliveryStatusService, DeliveryStatusService>();
            svc.AddScoped<IEmailService, EmailService>();
            svc.AddScoped<IFoodService, FoodService>();
            svc.AddScoped<IFeedbackService, FeedbackService>();
            svc.AddScoped<IImageService, ImageService>();
            svc.AddScoped<IIngredientService, IngredientService>();
            svc.AddScoped<IKeyService, KeyService>();
            svc.AddScoped<IPermissionsService, PermissionsService>();
            svc.AddScoped<IOrderService, OrderService>();
            svc.AddScoped<IPaymentTypeService, PaymentTypeService>();
            svc.AddScoped<ISuborderService, SuborderService>();
            svc.AddScoped<ITokenJWTService, TokenJWTService>();
            svc.AddScoped<IUserService, UserService>();
            //Repositories
            svc.AddScoped<IAddressRepository, AddressRepository>();
            svc.AddScoped<IAddressTypeRepository, AddressTypeRepository>();
            svc.AddScoped<ICategoryRepository, CategoryRepository>();
            svc.AddScoped<IChangeRepository, ChangeRepository>();
            svc.AddScoped<IContactRepository, ContactRepository>();
            svc.AddScoped<IDeliveryStatusRepository, DeliveryStatusRepository>();
            svc.AddScoped<IFoodRepository, FoodRepository>();
            svc.AddScoped<IFeedbackRepository, FeedbackRepository>();
            svc.AddScoped<IImageRepository, ImageRepository>();
            svc.AddScoped<IIngredientRepository, IngredientRepository>();
            svc.AddScoped<IKeyRepository, KeyRepository>();
            svc.AddScoped<IOrderRepository, OrderRepository>();
            svc.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
            svc.AddScoped<ISuborderRepository, SuborderRepository>();
            svc.AddScoped<ITokenJWTRepository, TokenJWTRepository>();
            svc.AddScoped<IUserRepository, UserRepository>();

            //Authorizations
            svc.AddSingleton<IAuthorizationHandler, EmailVerifiedHandler>();

            return svc;
        }

        public static IServiceCollection AddCorPolicies(this IServiceCollection svc, IConfiguration cfg)
        {
            //Cors Policy
            svc.AddCors(options =>
            {
                options.AddPolicy("WebPolicy", policy =>
                {
                    policy.AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .SetIsOriginAllowed(hostName => true);
                });
            });

            return svc;
        }
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection svc, IConfiguration cfg)
        {
            svc.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                var JWTBearer = cfg.GetSection("Auth:JWTBearer");
                option.RequireHttpsMetadata = false;
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTBearer["SecretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                option.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            svc.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.EmailVerified, policy =>
                                                        policy.RequireAssertion(context => context.User.IsInRole(Policy.Admin) || context.User.HasClaim(c => c.Type == Policy.EmailVerified && c.Value == true.ToString())));
                options.AddPolicy(Policy.Admin, policy => policy.RequireRole(Policy.Admin));
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

            return svc;
        }
    }
}