using Kros.Utils;
using Microsoft.AspNetCore.Http;
using PostmanTesting.Infrastructure;
using System;
using System.Linq;
using System.Security.Claims;

namespace PostmanTesting.Application.Services
{
    /// <summary>
    /// Service providing info about currently logged user.
    /// </summary>
    public class ActiveUserInfoService : IActiveUserInfoService
    {
        private readonly ClaimsPrincipal _principal;
        private readonly Lazy<long> _userId;
        private readonly Lazy<string> _userRole;

        /// <inheritdoc/>
        public long UserId => _userId.Value;

        /// <inheritdoc/>
        public bool IsAdmin => _userRole.Value == Policies.AdminPolicyName;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="contextAccessor"><see cref="IHttpContextAccessor"/></param>
        public ActiveUserInfoService(IHttpContextAccessor contextAccessor)
        {
            _principal = Check.NotNull(contextAccessor.HttpContext.User, nameof(contextAccessor));
            _userId = new Lazy<long>(GetUserId);
            _userRole = new Lazy<string>(GetUserRole);
        }

        private long GetUserId()
        {
            string userId = _principal.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            if (long.TryParse(userId, out long id))
            {
                return id;
            }
            else
            {
                return -1;
            }
        }

        private string GetUserRole()
        {
            string userRole = _principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty;

            if (!string.IsNullOrEmpty(userRole))
            {
                return userRole;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
