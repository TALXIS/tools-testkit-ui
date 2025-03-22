﻿namespace TALXIS.TestKit.Bindings.Configuration
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Test configuration for PowerApps UI testing.
    /// </summary>
    public class TestConfiguration
    {
        private readonly IConfiguration configuration;
        private static Dictionary<string, UserConfiguration> currentUsers = new Dictionary<string, UserConfiguration>();

        public TestConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.BrowserOptions = configuration.GetSection("BrowserOptions").Get<BrowserOptionsWithProfileSupport>();
            this.Users = configuration.GetSection("Users").Get<List<UserConfiguration>>();
            this.Url = configuration["Url"];
            this.UseProfiles = configuration.GetValue<bool>("UseProfiles");
            this.DeleteTestData = configuration.GetValue<bool>("DeleteTestData");
            this.ProfilesBasePath = configuration["ProfilesBasePath"];
            this.ApplicationUser = configuration.GetSection("applicationUser").Get<ClientCredentials>();

            // TODO: Make this overrideable from config
            //this.BrowserOptions.DriversPath
        }

        /// <summary>
        /// Gets or sets the URL of the target PowerApps instance.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use profiles.
        /// </summary>
        public bool UseProfiles { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to delete test data.
        /// </summary>
        public bool DeleteTestData { get; set; } = true;

        /// <summary>
        /// Gets or sets the base path where the user profiles are stored.
        /// </summary>
        public string ProfilesBasePath { get; set; }
        public BrowserOptionsWithProfileSupport BrowserOptions { get; set; }
        public List<UserConfiguration> Users { get; set; }

        public ClientCredentials ApplicationUser { get; set; }


        /// <summary>
        /// Gets the target URL.
        /// </summary>
        /// <returns>The URL of the test environment.</returns>
        public Uri GetTestUrl()
        {
            return new Uri(this.Url);
        }

        public UserConfiguration GetUser(string userAlias, bool useCurrentUser = true)
        {
            if (useCurrentUser && currentUsers.ContainsKey(userAlias))
            {
                return currentUsers[userAlias];
            }

            var user = this.Users.Find(u => u.Alias == userAlias);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            currentUsers[userAlias] = user;
            return user;
        }

        /// <summary>
        /// Called internally between scenarios to reset thread state.
        /// </summary>
        internal void Flush()
        {
            currentUsers.Clear();
        }
    }
}
