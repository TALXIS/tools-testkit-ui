﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;

namespace TALXIS.TestKit.Selectors.Browser
{
    public static class BrowserDriverFactory
    {
        public static IWebDriver CreateWebDriver(BrowserOptions options)
        {
            DriverManager driverManager = new();

            IWebDriver driver;
            
            switch (options.BrowserType)
            {
                case BrowserType.Chrome:
                    driverManager.SetUpDriver(new ChromeConfig(), VersionResolveStrategy.Latest);
                    var chromeService = ChromeDriverService.CreateDefaultService();
                    chromeService.HideCommandPromptWindow = options.HideDiagnosticWindow;
                    driver = new ChromeDriver(chromeService, options.ToChrome(), options.CommandTimeout);
                    break;
                case BrowserType.IE:
                    driverManager.SetUpDriver(new InternetExplorerConfig(), VersionResolveStrategy.Latest);
                    var ieService = InternetExplorerDriverService.CreateDefaultService(options.DriversPath);
                    ieService.SuppressInitialDiagnosticInformation = options.HideDiagnosticWindow;
                    driver = new InternetExplorerDriver(ieService, options.ToInternetExplorer(), options.CommandTimeout);
                    break;
                case BrowserType.Firefox:
                    driverManager.SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.Latest);
                    var ffService = FirefoxDriverService.CreateDefaultService(options.DriversPath);
                    ffService.HideCommandPromptWindow = options.HideDiagnosticWindow;
                    driver = new FirefoxDriver(ffService, options.ToFireFox());
                    driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 5);
                    break;
                case BrowserType.Edge:
                    driverManager.SetUpDriver(new EdgeConfig(), VersionResolveStrategy.Latest);
                    var edgeService = EdgeDriverService.CreateDefaultService(options.DriversPath);
                    edgeService.HideCommandPromptWindow = options.HideDiagnosticWindow;
                    driver = new EdgeDriver(edgeService, options.ToEdge(), options.CommandTimeout);
                    break;
                case BrowserType.Remote:
                    ICapabilities capabilities = null;
                    switch (options.RemoteBrowserType)
                    {
                        case BrowserType.Chrome:
                            capabilities = options.ToChrome().ToCapabilities();
                            break;
                        case BrowserType.Firefox:
                            capabilities = options.ToFireFox().ToCapabilities();
                            break;
                    }
                    driver = new RemoteWebDriver(options.RemoteHubServer, capabilities, options.CommandTimeout);
                    break;
                default:
                    throw new InvalidOperationException(
                        $"The browser type '{options.BrowserType}' is not recognized.");
            }

            driver.Manage().Timeouts().PageLoad = options.PageLoadTimeout;

            // StartMaximized overrides a set width & height
            if (options.StartMaximized && options.BrowserType != BrowserType.Chrome) //Handle Chrome in the Browser Options
                driver.Manage().Window.Maximize();
            else if (!options.StartMaximized && options.Width.HasValue && options.Height.HasValue)
                driver.Manage().Window.Size = new System.Drawing.Size(options.Width.Value, options.Height.Value);

            if (options.FireEvents || options.EnableRecording)
            {
                // Wrap the newly created driver.
                driver = new EventFiringWebDriver(driver);
            }

            return driver;
        }
    }
}