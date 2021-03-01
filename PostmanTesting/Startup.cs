using Kros.AspNetCore;
using Kros.Swagger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            services.AddSwaggerDocumentation(Configuration);
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
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
