namespace PostmanTesting.Options
{
    /// <summary>
    /// Settings for API JWT authorization.
    /// </summary>
    public class ApiJwtAuthorizationSettings
    {
        /// <summary>
        /// Scheme name.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Authorization authority.
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// Required scope.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Flag if HTTPS metadata is required.
        /// </summary>
        public bool RequireHttpsMetadata { get; set; }
    }
}
