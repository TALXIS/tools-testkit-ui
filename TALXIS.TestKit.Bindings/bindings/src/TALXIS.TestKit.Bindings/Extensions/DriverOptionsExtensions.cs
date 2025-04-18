﻿namespace TALXIS.TestKit.Bindings.Extensions
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.IE;

    /// <summary>
    /// Extensions to the <see cref="DriverOptions"/> class.
    /// </summary>
    public static class DriverOptionsExtensions
    {
        /// <summary>
        /// Adds a global capability to driver options.
        /// </summary>
        /// <param name="options">The driver options.</param>
        /// <param name="name">The name of the capability.</param>
        /// <param name="value">The value of the capability.</param>
        internal static void AddGlobalCapability(this DriverOptions options, string name, object value)
        {
            switch (options)
            {
                case ChromeOptions chromeOptions:
                    chromeOptions.AddAdditionalChromeOption(name, value);
                    break;
                case FirefoxOptions firefoxOptions:
                    firefoxOptions.AddAdditionalFirefoxOption(name, value);
                    break;
                case InternetExplorerOptions internetExplorerOptions:
                    internetExplorerOptions.AddAdditionalInternetExplorerOption(name, value);
                    break;
                default:
                    options.AddAdditionalOption(name, value);
                    break;
            }
        }
    }
}
