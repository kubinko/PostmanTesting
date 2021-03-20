using Kros.KORM;
using Kros.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;

namespace PostmanTesting.Infrastructure
{
    /// <summary>
    /// Generates current user ID.
    /// </summary>
    public class CurrentUserValueGenerator : IValueGenerator
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        public CurrentUserValueGenerator(IServiceProvider serviceProvider)
        {
            _serviceProvider = Check.NotNull(serviceProvider, nameof(serviceProvider));
        }

        /// <inheritdoc />
        public object GetValue()
        {
            using var scope = _serviceProvider.CreateScope();
            var principal = scope.ServiceProvider.GetService<IHttpContextAccessor>();
            string userId = principal.HttpContext.User.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            return long.TryParse(userId, out long id) ? id : -1;
        }
    }
}
