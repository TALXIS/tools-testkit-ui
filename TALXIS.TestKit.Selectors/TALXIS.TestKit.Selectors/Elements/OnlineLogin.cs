// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System;
using System.Security;
using TALXIS.TestKit.Selectors.WebClientManagement;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors
{
    public class OnlineLogin : Element
    {
        private readonly LoginManager _manager;

        public OnlineLogin(WebClient client)
        {
            _manager = new LoginManager(client);
        }

        /// <summary>
        /// Logs into the organization without providing a username and password.  This login action will use pass through authentication and automatically log you in. 
        /// </summary>
        /// <param name="orgUrl">URL of the organization</param>
        public void Login(Uri orgUrl)
        {
            LoginResult result = _manager.Login(orgUrl);
            if (result == LoginResult.Failure)
                throw new InvalidOperationException("Login Failure, please check your configuration");

            _manager.Client.InitializeModes();
        }

        /// <summary>
        /// Logs into the organization with the user and password provided
        /// </summary>
        /// <param name="orgUrl">URL of the organization</param>
        /// <param name="username">User name</param>
        /// <param name="password">Password</param>
        /// <param name="mfaSecretKey">SecretKey for multi-factor authentication</param>
        public void Login(Uri orgUrl, SecureString username, SecureString password, SecureString mfaSecretKey = null)
        {
            LoginResult result = _manager.Login(orgUrl, username, password, mfaSecretKey);
            if (result == LoginResult.Failure)
                throw new InvalidOperationException("Login Failure, please check your configuration");

            _manager.Client.InitializeModes();
        }

        /// <summary>
        /// Logs into the organization with the user and password provided
        /// </summary>
        /// <param name="orgUrl">URL of the organization</param>
        /// <param name="username">User name</param>
        /// <param name="password">Password</param>
        /// <param name="mfaSecretKey">SecretKey for multi-factor authentication</param>
        /// <param name="redirectAction">Actions required during redirect</param>
        public void Login(Uri orgUrl, SecureString username, SecureString password, SecureString mfaSecretKey, Action<LoginRedirectEventArgs> redirectAction)
        {
            LoginResult result = _manager.Login(orgUrl, username, password, mfaSecretKey, redirectAction);
            if (result == LoginResult.Failure)
                throw new InvalidOperationException("Login Failure, please check your configuration");

            _manager.Client.InitializeModes();
        }
    }
}
