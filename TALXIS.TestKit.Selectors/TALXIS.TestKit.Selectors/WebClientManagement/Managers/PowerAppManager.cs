using System;
using System.Diagnostics;
using OpenQA.Selenium;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors.WebClientManagement
{
    public class PowerAppManager
    {
        private bool _inPowerApps;
        public WebClient Client { get; }

        public PowerAppManager(WebClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));

            _inPowerApps = false;
        }

        public BrowserCommandResult<bool> PowerAppSelect(string appId, string control)
        {

            return this.Client.Execute(Client.GetOptions("PowerApp Select"), driver =>
            {
                if (!_inPowerApps) LocatePowerApp(driver, appId);
                if (driver.HasElement(By.XPath(AppElements.Xpath[AppReference.PowerApp.Control].Replace("[NAME]", control))))
                {
                    driver.FindElement(By.XPath(AppElements.Xpath[AppReference.PowerApp.Control].Replace("[NAME]", control))).Click();
                }
                else
                {
                    throw new NotFoundException(String.Format("Control {0} not found in Power App {1}", control, appId));
                }
                return true;
            });
        }

        public BrowserCommandResult<bool> PowerAppSendCommand(string appId, string command)
        {
            return this.Client.Execute(Client.GetOptions("PowerApp Send Command"), driver =>
            {
                LocatePowerApp(driver, appId);
                return true;
            });
        }

        public BrowserCommandResult<bool> PowerAppSetProperty(string appId, string control, string value)
        {

            return this.Client.Execute(Client.GetOptions("PowerApp Set Property"), driver =>
            {
                LocatePowerApp(driver, appId);
                return true;
            });
        }

        internal IWebElement LocatePowerApp(IWebDriver driver, string appId)
        {
            IWebElement powerApp = null;
            Trace.WriteLine(String.Format("Locating {0} App", appId));
            if (driver.HasElement(By.XPath(AppElements.Xpath[AppReference.PowerApp.ModelFormContainer].Replace("[NAME]", appId))))
            {
                powerApp = driver.FindElement(By.XPath(AppElements.Xpath[AppReference.PowerApp.ModelFormContainer].Replace("[NAME]", appId)));
                driver.SwitchTo().Frame(powerApp);
                powerApp = driver.FindElement(By.XPath("//iframe[@class='publishedAppIframe']"));
                driver.SwitchTo().Frame(powerApp);
                _inPowerApps = true;
            }
            else
            {
                throw new NotFoundException(String.Format("PowerApp with Id {0} not found.", appId));
            }
            return powerApp;
        }

        #region Copilot
        private bool _coPilotEnabled = false;
        internal BrowserCommandResult<string> AskAQuestion(string userInput)
        {
            return this.Client.Execute(Client.GetOptions($"Ask A Question for Copilot"), driver =>
            {
                if (!_coPilotEnabled) EnableCustomerServiceCopilot(driver);
                IWebElement relatedEntity = null;

                if (driver.TryFindElement(By.XPath(""), out var copilot))
                {
                    // Advanced lookup
                    //relatedEntity = advancedLookup.WaitUntilAvailable(
                    //    By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.FilterTable].Replace("[NAME]", entityName)),
                    //    2.Seconds());
                }
                else
                {
                    // Lookup 
                    //relatedEntity = driver.WaitUntilAvailable(
                    //    By.XPath(AppElements.Xpath[AppReference.Lookup.RelatedEntityLabel].Replace("[NAME]", entityName)),
                    //    2.Seconds());
                }

                //if (relatedEntity == null)
                //{
                //    throw new NotFoundException($"Lookup Entity {entityName} not found.");
                //}

                //relatedEntity.Click();
                driver.WaitForTransaction();

                return string.Empty;
            });
        }

        internal BrowserCommandResult<bool> EnableAskAQuestion()
        {
            return this.Client.Execute(Client.GetOptions($"Enable Ask A Question for Copilot"), driver =>
            {
                if (!_coPilotEnabled) EnableCustomerServiceCopilot(driver);

                IWebElement relatedEntity = null;

                if (driver.TryFindElement(By.XPath(""), out var copilot))
                {
                    // Advanced lookup
                    //relatedEntity = advancedLookup.WaitUntilAvailable(
                    //    By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.FilterTable].Replace("[NAME]", entityName)),
                    //    2.Seconds());
                }
                else
                {
                    // Lookup 
                    //relatedEntity = driver.WaitUntilAvailable(
                    //    By.XPath(AppElements.Xpath[AppReference.Lookup.RelatedEntityLabel].Replace("[NAME]", entityName)),
                    //    2.Seconds());
                }

                //if (relatedEntity == null)
                //{
                //    throw new NotFoundException($"Lookup Entity {entityName} not found.");
                //}

                //relatedEntity.Click();
                driver.WaitForTransaction();

                return true;
            });
        }

        internal IWebElement EnableCustomerServiceCopilot(IWebDriver driver)
        {
            IWebElement powerApp = null;
            Trace.WriteLine("Locating Copilot");
            if (driver.HasElement(By.XPath(AppElements.Xpath[AppReference.PowerApp.ModelFormContainer])))
            {
                powerApp = driver.FindElement(By.XPath(AppElements.Xpath[AppReference.PowerApp.ModelFormContainer]));
                driver.SwitchTo().Frame(powerApp);
                powerApp = driver.FindElement(By.XPath("//iframe[@class='publishedAppIframe']"));
                driver.SwitchTo().Frame(powerApp);
                _inPowerApps = true;
            }
            else
            {
                throw new NotFoundException("Copilot not found or not enabled.");
            }
            return powerApp;
        }
        #endregion
    }
}
