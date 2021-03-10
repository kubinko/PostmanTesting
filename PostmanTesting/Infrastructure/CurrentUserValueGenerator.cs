using Kros.KORM;
using Kros.Utils;
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
            var principal = scope.ServiceProvider.GetService<ClaimsPrincipal>();
            string userId = principal.Claims.FirstOrDefault(p => p.Type == "sub")?.Value ?? string.Empty;

            return long.TryParse(userId, out long id) ? id : -1;
        }
    }
}
