using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NavigationModule.Authentication.Brokers.DateTimes;
using NavigationModule.Authentication.Brokers.Managements.Authentications;
using NavigationModule.Authentication.Brokers.Managements.Roles;
using NavigationModule.Authentication.Brokers.Managements.Users;
using NavigationModule.Authentication.Brokers.Storages;
using NavigationModule.Authentication.Models.Configurations;
using NavigationModule.Authentication.Models.Entities.Roles;
using NavigationModule.Authentication.Models.Entities.Users;
using NavigationModule.Authentication.Services.Foundations.Authentications;
using NavigationModule.Authentication.Services.Foundations.Roles;
using NavigationModule.Authentication.Services.Foundations.Users;
using NavigationModule.Authentication.Services.Orchestrations;
using NavigationModule.Authentication.Services.Processings.Authentications;
using NavigationModule.Authentication.Services.Processings.Roles;
using NavigationModule.Authentication.Services.Processings.Users;

namespace NavigationModule.Extensions
{
    public static partial class ServicesExtensions
    {
        public static void AddCustomAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AuthConfiguration authConfiguration = configuration
                .GetSection(nameof(AuthConfiguration))
                .Get<AuthConfiguration>();

            PasswordConfiguration passwordConfiguration = configuration
                .GetSection(nameof(PasswordConfiguration))
                .Get<PasswordConfiguration>();

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = passwordConfiguration.RequiredLength;
                options.Password.RequireDigit = passwordConfiguration.RequireDigit;
                options.Password.RequireLowercase = passwordConfiguration.RequireLowercase;
                options.Password.RequireUppercase = passwordConfiguration.RequireUppercase;
                options.Password.RequireNonAlphanumeric = passwordConfiguration.RequireNonAlphanumeric;
            })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<StorageBroker>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = authConfiguration.ValidateIssuer,
                        ValidateAudience = authConfiguration.ValidateAudience,
                        ValidateIssuerSigningKey = authConfiguration.ValidateIssuerSigningKey,
                        RequireExpirationTime = authConfiguration.RequireExpirationTime,
                        ValidateLifetime = authConfiguration.ValidateLifetime,
                        RequireSignedTokens = authConfiguration.RequireSignedTokens,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(authConfiguration.SigningKey)),
                    };
                });
        }

        public static void AddAuthenticationContext(this IServiceCollection services)
        {
            services.AddDbContext<StorageBroker>();
        }

        public static void AddAuthenticationBrokers(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeBroker, DateTimeBroker>();
            services.AddScoped<IStorageBroker, StorageBroker>();
            services.AddScoped<IUserManagerBroker, UserManagerBroker>();
            services.AddScoped<IRoleManagerBroker, RoleManagerBroker>();
            services.AddScoped<IAuthenticationManagerBroker, AuthenticationManagerBroker>();
        }

        public static void AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IRoleService, RoleService>();

            services.AddTransient<IAuthenticationProcessingService, AuthenticationProcessingService>();
            services.AddTransient<IUserProcessingService, UserProcessingService>();
            services.AddTransient<IRoleProcessingService, RoleProcessingService>();

            services.AddTransient<IUserOrchestrationService, UserOrchestrationService>();
        }

        public static void AddCustomHealthChecks(
            this IHealthChecksBuilder healthChecksBuilder,
            IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(
                name: "DefaultConnection");

            healthChecksBuilder
                .AddDbContextCheck<StorageBroker>(nameof(StorageBroker))
                .AddNpgSql(
                    connectionString,
                    name: "PostgreSQL");
        }
    }
}