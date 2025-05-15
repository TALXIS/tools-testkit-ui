namespace TALXIS.TestKit.Bindings.Steps
{
    using System;
    using OpenQA.Selenium;
    using Reqnroll;
    using TALXIS.TestKit.Selectors;
    using TALXIS.TestKit.Selectors.Browser;
    using TALXIS.TestKit.Bindings.Configuration;
    using TALXIS.TestKit.Bindings.Extensions.CookiesManagement;

    /// <summary>
    /// Step bindings related to logging in.
    /// </summary>
    [Binding]
    public class LoginSteps : PowerAppsStepDefiner
    {
        /// <summary>
        /// Logs in to a given app as a given user.
        /// </summary>
        /// <param name="appName">The name of the app.</param>
        /// <param name="userAlias">The alias of the user.</param>
        [Given("I am logged in to the '(.*)' app as '(.*)'")]
        public static void GivenIAmLoggedInToTheAppAs(string appName, string userAlias)
        {
            var user = TestConfig.GetPersona(userAlias, useCurrentUser: false);


            var url = TestConfig.GetTestUrl();
            var driver = Driver;
            /*
            var roleAssignmentService = new RoleAssignmentService(
                DataverseServiceClientFactory.CreateWithClientCredentials(
                    TestConfig.Url,
                    TestConfig.ApplicationUser));
             */
            var roleAssignmentService = new RoleAssignmentService(
                DataverseServiceClientFactory.CreateWithToken(
                    TestConfig.Url,
                    AccessToken));

            roleAssignmentService.UpdateSecurityRoles(user.Username, user.SecurityRoles);

            Login(driver, url, user);

            if (!url.Query.Contains("appid"))
            {
                XrmApp.Navigation.OpenApp(appName);
            }

            Driver.WaitForTransaction();

            CloseTeachingBubbles();

            WaitForMainPage(driver);
        }

        private static void Login(IWebDriver driver, Uri url, PersonaConfiguration user)
        {
            if (!CookieManager.UserLoginCookies.ContainsKey(user.Username))
            {
                if (!CookieManager.TryLoadCookies(driver, user.Username))
                {
                    LoginViaUi(url, user);

                    CookieManager.SaveCookies(driver, url, user.Username);
                }
                else
                {
                    CookieManager.LoginViaCookies(driver, url, user.Username);
                }
            }
            else
            {
                CookieManager.LoginViaCookies(driver, url, user.Username);
            }
        }

        private static void LoginViaUi(Uri url, PersonaConfiguration user)
        {
            if (!string.IsNullOrEmpty(user.OtpToken))
            {
                XrmApp.OnlineLogin.Login(url, user.Username.ToSecureString(), user.Password.ToSecureString(), user.OtpToken.ToSecureString());
            }
            else
            {
                XrmApp.OnlineLogin.Login(url, user.Username.ToSecureString(), user.Password.ToSecureString());
            }
        }

        private static void CloseTeachingBubbles()
        {
            foreach (var closeButton in Driver.FindElements(By.ClassName("ms-TeachingBubble-closebutton")))
            {
                closeButton.Click();
            }
        }

        private static bool WaitForMainPage(IWebDriver driver, TimeSpan? timeout = null)
        {
            timeout = timeout ?? TimeSpan.FromSeconds(15);
            var isUCI = driver.HasElement(By.XPath(Elements.Xpath[Reference.Login.CrmUCIMainPage]));

            if (isUCI) driver.WaitForTransaction();

            var element = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Login.CrmMainPage]), timeout);

            return element != null;
        }
    }
}
