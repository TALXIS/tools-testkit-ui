namespace TALXIS.TestKit.Bindings.Configuration
{
    /// <summary>
    /// Configuration for the test application user.
    /// </summary>
    public class ClientCredentials
    {
        /// <summary>
        /// Gets or sets the tenant ID of the test application user app registration.
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the client ID of the test application user app registration.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets a client secret or the name of an environment variable containing the client secret of the test application user app registration.
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
