﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Security;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors
{
    public class LoginRedirectEventArgs : EventArgs
    {
        protected internal LoginRedirectEventArgs(SecureString username, SecureString password, IWebDriver driver)
        {
            this.Username = username;
            this.Password = password;
            this.Driver = driver;
        }

        public SecureString Username { get; private set; }
        public SecureString Password { get; private set; }
        public IWebDriver Driver { get; private set; }
    }
}