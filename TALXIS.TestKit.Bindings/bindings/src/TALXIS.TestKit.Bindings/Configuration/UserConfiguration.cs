namespace TALXIS.TestKit.Bindings.Configuration
{
    /// <summary>
    /// A user that tests can run as.
    /// </summary>
    public class UserConfiguration
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the alias of the user (used to retrieve from configuration).
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the OTP token of the user.
        /// </summary>
        public string OtpToken { get; set; }
    }
}