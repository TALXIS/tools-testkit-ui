using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TALXIS.TestKit.Selectors.DTO.Locators;
using TALXIS.TestKit.Selectors.Browser;
namespace TALXIS.TestKit.Selectors.WebClientManagement.Helpers
{
    internal class ActionHelper
    {
        internal static void ClickTab(IWebElement tabList, string xpath, string name, WebClient client)
        {
            IWebElement moreTabsButton;
            IWebElement listItem;
            // Look for the tab in the tab list, else in the more tabs menu
            IWebElement searchScope = null;
            if (tabList.HasElement(By.XPath(string.Format(xpath, name))))
            {
                searchScope = tabList;
            }
            else if (tabList.TryFindElement(EntityElementsLocators.MoreTabs, out moreTabsButton))
            {
                moreTabsButton.Click();

                // No tab to click - subtabs under 'Related' are automatically expanded in overflow menu
                if (name == "Related")
                {
                    return;
                }
                else
                {
                    searchScope = client.Browser.Driver.FindElement(EntityElementsLocators.MoreTabsMenu);
                }
            }

            if (searchScope.TryFindElement(By.XPath(string.Format(xpath, name)), out listItem))
            {
                listItem.Click(true);
            }
            else
            {
                throw new Exception($"The tab with name: {name} does not exist");
            }
        }

        internal static BrowserCommandResult<bool> CloseActivity(bool closeOrCancel, WebClient client, int thinkTime = Constants.DefaultThinkTime)
        {
            client.ThinkTime(thinkTime);

            var xPathQuery = closeOrCancel
                ? AppElements.Xpath[AppReference.Dialogs.CloseActivity.Close]
                : AppElements.Xpath[AppReference.Dialogs.CloseActivity.Cancel];

            var action = closeOrCancel ? "Close" : "Cancel";

            return client.Execute(client.GetOptions($"{action} Activity"), driver =>
            {
                var dialog = driver.WaitUntilAvailable(DialogsElementsLocators.DialogContext);

                var actionButton = dialog.FindElement(By.XPath(xPathQuery));

                actionButton?.Click();

                driver.WaitForTransaction();

                return true;
            });
        }

        internal static BrowserCommandResult<bool> HandleSaveDialog(WebClient client)
        {
            //If you click save and something happens, handle it.  Duplicate Detection/Errors/etc...
            //Check for Dialog and figure out which type it is and return the dialog type.

            //Introduce think time to avoid timing issues on save dialog
            client.ThinkTime(1000);

            return client.Execute(client.GetOptions($"Validate Save"), driver =>
            {
                //Is it Duplicate Detection?
                if (driver.HasElement(EntityElementsLocators.DuplicateDetectionWindowMarker))
                {
                    if (driver.HasElement(EntityElementsLocators.DuplicateDetectionGridRows))
                    {
                        //Select the first record in the grid
                        driver.FindElements(EntityElementsLocators.DuplicateDetectionGridRows)[0].Click(true);

                        //Click Ignore and Save
                        driver.FindElement(EntityElementsLocators.DuplicateDetectionIgnoreAndSaveButton).Click(true);
                        driver.WaitForTransaction();
                    }
                }

                //Is it an Error?
                if (driver.HasElement(By.XPath("//div[contains(@data-id,'errorDialogdialog')]")))
                {
                    var errorDialog = driver.FindElement(By.XPath("//div[contains(@data-id,'errorDialogdialog')]"));

                    var errorDetails = errorDialog.FindElement(By.XPath(".//*[contains(@data-id,'errorDialog_subtitle')]"));

                    if (!String.IsNullOrEmpty(errorDetails.Text))
                        throw new InvalidOperationException(errorDetails.Text);
                }


                return true;
            });
        }

        /// <summary>
        /// Generic method to help click on any item which is clickable or uniquely discoverable with a By object.
        /// </summary>
        /// <param name="by">The xpath of the HTML item as a By object</param>
        /// <returns>True on success, Exception on failure to invoke any action</returns>
        internal static BrowserCommandResult<bool> SelectTab(string tabName, WebClient client, string subTabName = "", int thinkTime = Constants.DefaultThinkTime)
        {
            client.ThinkTime(thinkTime);

            return client.Execute($"Select Tab", driver =>
            {
                driver.WaitUntilVisible(By.CssSelector($"li[title=\"{tabName}\"]"));

                IWebElement tabList;
                if (driver.HasElement(DialogsElementsLocators.DialogContext))
                {
                    var dialogContainer = driver.FindElement(DialogsElementsLocators.DialogContext);
                    tabList = dialogContainer.WaitUntilAvailable(EntityElementsLocators.TabList);
                }
                else
                {
                    tabList = driver.WaitUntilAvailable(EntityElementsLocators.TabList);
                }

                ClickTab(tabList, AppElements.Xpath[AppReference.Entity.Tab], tabName, client);

                //Click Sub Tab if provided
                if (!String.IsNullOrEmpty(subTabName))
                {
                    ClickTab(tabList, AppElements.Xpath[AppReference.Entity.SubTab], subTabName, client);
                }

                driver.WaitForTransaction();
                return true;
            });
        }

        internal static void SaveTheRecord(WebClient client)
        {
            client.Execute<object>("Save The Record", driver =>
            {
                client.ThinkTime(5000);
                try
                {
                    Actions actions = new Actions(driver);

                    actions.SendKeys(Keys.Escape).Perform();
                    actions.KeyDown(Keys.Control)
                           .SendKeys("S")
                           .KeyUp(Keys.Control)
                           .Perform();
                }
                catch (InvalidOperationException)
                {
                    // Ignore business process errors on save to allow assertions against these if required.
                }

                driver.WaitForTransaction();
                driver.WaitForPageToLoad();

                return null;
            });
        }
    }
}
