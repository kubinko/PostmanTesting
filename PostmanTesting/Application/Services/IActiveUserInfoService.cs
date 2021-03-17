namespace PostmanTesting.Application.Services
{
    /// <summary>
    /// Service providing info about currently logged user.
    /// </summary>
    public interface IActiveUserInfoService
    {
        /// <summary>
        /// Logged user ID.
        /// </summary>
        long UserId { get; }

        /// <summary>
        /// Whether user has role Admin.
        /// </summary>
        /// <returns>
        /// <c>true</c> if current user has role Admin; otherwise, <c>false</c>.
        /// </returns>
        bool IsAdmin { get; }
    }
}