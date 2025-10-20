using System;
using System.Collections;
using System.Collections.Generic;

namespace TALXIS.TestKit.Bindings.Configuration
{
    public class PersonaConfiguration
    {
        /// <summary>
        /// Gets or sets the username of the pesrona.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the pesrona.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the OTP token of the pesrona.
        /// </summary>
        public string OtpToken { get; set; }

        /// <summary>
        /// Gets or sets the alias of the pesrona (used to retrieve from configuration).
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the Security Roles of the pesrona.
        /// </summary>
        public List<Guid> SecurityRoles { get; set; }
    }
}
