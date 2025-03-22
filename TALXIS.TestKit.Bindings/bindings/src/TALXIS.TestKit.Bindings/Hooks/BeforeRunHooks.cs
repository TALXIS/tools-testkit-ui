namespace TALXIS.TestKit.Bindings.Hooks
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Reqnroll;
    using TALXIS.TestKit.Bindings.Configuration;
    using TALXIS.TestKit.Selectors;
    using TALXIS.TestKit.Selectors.WebClientManagement;
    using TALXIS.TestKit.Selectors.Browser;

    /// <summary>
    /// Hooks that run before the start of each run.
    /// </summary>
    [Binding]
    public class BeforeRunHooks : PowerAppsStepDefiner
    {
        /// <summary>
        /// Creates a new folder for the scenario and copies the session/cookies information from previous runs.
        /// </summary>
        [BeforeTestRun]
        public static void BaseProfileSetup()
        {
            if (!TestConfig.UseProfiles)
            {
                return;
            }

            Parallel.ForEach(UserProfileDirectories.Keys, (username) =>
            {
                var profileDirectory = UserProfileDirectories[username];
                var baseDirectory = Path.Combine(profileDirectory, "base");

                // Reqnroll isolation settings may run scenarios in different processes or AppDomains and [BeforeTestRun] runs per thread. Lock statement insufficient.
                using (var mutex = new Mutex(true, $"{nameof(BaseProfileSetup)}-{username}", out var createdNew))
                {
                    if (!createdNew)
                    {
                        mutex.WaitOne();
                    }

                    if (Directory.Exists(baseDirectory))
                    {
                        mutex.ReleaseMutex();
                        return;
                    }

                    try
                    {
                        Directory.CreateDirectory(baseDirectory);

                        var userBrowserOptions = (BrowserOptionsWithProfileSupport)TestConfig.BrowserOptions.Clone();
                        userBrowserOptions.ProfileDirectory = baseDirectory;
                        userBrowserOptions.Headless = false;

                        var webClient = new WebClient(userBrowserOptions);
                        using (var app = new XrmApp(webClient))
                        {
                            var user = TestConfig.Users.First(u => u.Username == username);
                            app.OnlineLogin.Login(TestConfig.GetTestUrl(), user.Username.ToSecureString(), user.Password.ToSecureString());
                        }
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            });
        }
    }
}