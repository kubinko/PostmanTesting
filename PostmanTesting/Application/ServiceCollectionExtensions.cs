using FluentValidation.AspNetCore;
using Kros.KORM.Extensions.Asp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using PostmanTesting.Application.Services;
using PostmanTesting.Domain;
using PostmanTesting.Infrastructure;
using PostmanTesting.Options;
using System.Reflection;
using System.Security.Claims;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for registering services for this project to the DI container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private const string ApiName = "Postman Testing API v1";

        /// <summary>
        /// Registers FluentValidation.
        /// </summary>
        /// <param name="builder">MVC builder.</param>
        /// <returns>MVC builder.</returns>
        public static IMvcBuilder AddFluentValidation(this IMvcBuilder builder)
            => builder.AddFluentValidation(o =>
            {
                o.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                o.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

        /// <summary>
        /// Adds authentication and authorization.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <param name="settings">Authentication settings.</param>
        public static void AddJwtAuthentication(
            this IServiceCollection services,
            ApiJwtAuthorizationSettings settings)
        {
            services.AddAuthentication(settings.Scheme)
                .AddJwtBearer(settings.Scheme, options =>
                {
                    options.Authority = settings.Authority;
                    options.RequireHttpsMetadata = settings.RequireHttpsMetadata;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", settings.Scope);
                });
                options.AddPolicy(Policies.AdminPolicyName, policyAdmin =>
                {
                    policyAdmin.AuthenticationSchemes.Add(settings.Scheme);
                    policyAdmin.RequireClaim(ClaimTypes.Role, Policies.AdminPolicyName);
                });
            });
        }

        /// <summary>
        /// Adds KORM.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <param name="configuration">Configuration.</param>
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        => services.AddKorm(configuration)
            .UseDatabaseConfiguration(new DatabaseConfiguration(services))
            .InitDatabaseForIdGenerator()
            .AddKormMigrations(o =>
            {
                o.AddFileScriptsProvider("SqlScripts");
            })
            .Migrate();

        /// <summary>
        /// Adds repositories.
        /// </summary>
        /// <param name="services">DI container.</param>
        public static void AddRepositories(this IServiceCollection services)
            => services
                .AddScoped<IWorkshopRepository, WorkshopRepository>()
                .AddScoped<IPeopleRepository, PersonRepository>()
                .AddScoped<IAttendanceRepository, AttendanceRepository>();

        /// <summary>
        /// Adds services.
        /// </summary>
        /// <param name="services">DI container.</param>
        public static void AddServices(this IServiceCollection services)
            => services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<IActiveUserInfoService, ActiveUserInfoService>()
                .AddScoped<IWorkshopService, WorkshopService>()
                .AddScoped<IPeopleService, PeopleService>()
                .AddScoped<IAttendanceService, AttendanceService>();

        /// <summary>
        /// Adds basic health checks.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <param name="configuration">Configuration.</param>
        public static void AddBasicHealthChecks(this IServiceCollection services, IConfiguration configuration)
            => services.AddHealthChecks()
                .AddCheck(ApiName, _ => HealthCheckResult.Healthy(), tags: new[] { "api" })
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    name: $" {ApiName} database",
                    tags: new[] { "db", "sql" });
    }
}
