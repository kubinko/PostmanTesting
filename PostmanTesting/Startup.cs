using Kros.AspNetCore;
using Kros.AspNetCore.HealthChecks;
using Kros.Swagger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PostmanTesting.Options;

namespace PostmanTesting
{
    public class Startup : BaseStartup
    {
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
            base.ConfigureServices(services);

            services
                .AddControllers()
                .AddFluentValidation();

            services.AddJwtAuthentication(Configuration.GetSection("ApiJwtAuthorization").Get<ApiJwtAuthorizationSettings>());

            services.ConfigureDatabase(Configuration);
            services.AddRepositories();
            services.AddServices();

            services.AddSwaggerDocumentation(Configuration);
            services.AddBasicHealthChecks(Configuration);
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Postman Testing API v1");
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
