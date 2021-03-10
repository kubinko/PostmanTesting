using Kros.AspNetCore;
using Kros.AspNetCore.HealthChecks;
using Kros.KORM.Extensions.Asp;
using Kros.Swagger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PostmanTesting.Infrastructure;
using PostmanTesting.Options;

namespace PostmanTesting
{
    public class Startup : BaseStartup
    {
        private const string ApiName = "Postman Testing API v1";

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        /// <param name="env">Environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
            : base(configuration, env)
        { }

        /// <summary>
        /// Configure IoC container.
        /// </summary>
        /// <param name="services">Service.</param>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ConfigureAuthentication(services);
            ConfigureDatabase(services);

            services.AddSwaggerDocumentation(Configuration);
            services
                .AddHealthChecks()
                    .AddCheck(ApiName, _ => HealthCheckResult.Healthy(), tags: new[] { "api" })
                    .AddSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                        name: $" {ApiName} database",
                        tags: new[] { "db", "sql" });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            var jwtOptions = Configuration.GetSection("ApiJwtAuthorization").Get<ApiJwtAuthorizationSettings>();
            services.AddAuthentication(jwtOptions.Scheme)
                .AddJwtBearer(jwtOptions.Scheme, options =>
                {
                    options.Authority = jwtOptions.Authority;
                    options.RequireHttpsMetadata = jwtOptions.RequireHttpsMetadata;

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
                    policy.RequireClaim("scope", jwtOptions.Scope);
                });
            });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddKorm(Configuration)
                .UseDatabaseConfiguration(new DatabaseConfiguration(services))
                .InitDatabaseForIdGenerator()
                .AddKormMigrations(o =>
                {
                    o.AddFileScriptsProvider("SqlScripts");
                })
                .Migrate();
        }

        /// <summary>
        /// Configure web api pipeline.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public override void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            base.Configure(app, loggerFactory);

            if (Environment.IsTestOrDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName);
                });
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                app.UseHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponseAsync
                });

                endpoints.MapControllers()
                    .RequireAuthorization("ApiScope"); ;
            });
        }
    }
}
