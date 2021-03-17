using Kros.Utils;
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
        /// <param name="principal"><see cref="ClaimsPrincipal"/></param>
        public ActiveUserInfoService(ClaimsPrincipal principal)
        {
            _principal = Check.NotNull(principal, nameof(principal));
            _userId = new Lazy<long>(GetUserId);
            _userRole = new Lazy<string>(GetUserRole);
        }

        private long GetUserId()
        {
            string userId = _principal.Claims
                .FirstOrDefault(c => c.Type == IdentityModel.JwtClaimTypes.Subject)?.Value ?? string.Empty;

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
