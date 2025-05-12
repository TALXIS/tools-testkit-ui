using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace TALXIS.TestKit.Bindings.Configuration
{
    /// <summary>
    /// Test configuration for PowerApps UI testing.
    /// </summary>
    public class TestConfiguration
    {
        private readonly IConfiguration configuration;
        private static Dictionary<string, PersonaConfiguration> currentPersona = new Dictionary<string, PersonaConfiguration>();

        public TestConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.BrowserOptions = configuration.GetSection("BrowserOptions").Get<BrowserOptionsWithProfileSupport>();
            this.Personas = configuration.GetSection("Personas").Get<List<PersonaConfiguration>>();
            this.Url = configuration["Url"];
            this.UseProfiles = configuration.GetValue<bool>("UseProfiles");
            this.DeleteTestData = configuration.GetValue<bool>("DeleteTestData");
            this.ProfilesBasePath = configuration["ProfilesBasePath"];
            this.ApplicationUser = configuration.GetSection("applicationUser").Get<ClientCredentials>();

            SpecifyUsersForPersonas(configuration.GetSection("Users").Get<List<UserConfiguration>>());

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
        public List<PersonaConfiguration> Personas { get; set; }
        public ClientCredentials ApplicationUser { get; set; }


        /// <summary>
        /// Gets the target URL.
        /// </summary>
        /// <returns>The URL of the test environment.</returns>
        public Uri GetTestUrl()
        {
            return new Uri(this.Url);
        }

        public PersonaConfiguration GetPersona(string userAlias, bool useCurrentUser = true)
        {
            if (useCurrentUser && currentPersona.ContainsKey(userAlias))
            {
                return currentPersona[userAlias];
            }

            var user = this.Personas.Find(u => u.Alias == userAlias);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            currentPersona[userAlias] = user;
            return user;
        }

        /// <summary>
        /// Called internally between scenarios to reset thread state.
        /// </summary>
        internal void Flush()
        {
            currentPersona.Clear();
        }

        private void SpecifyUsersForPersonas(List<UserConfiguration> users)
        {
            foreach (var persona in Personas)
            {
                var matchingUser = users.FirstOrDefault(u => u.Username == persona.Username);

                if (matchingUser != null)
                {
                    persona.Password = matchingUser.Password;
                    persona.OtpToken = matchingUser.OtpToken;
                }
            }
        }
    }
}
