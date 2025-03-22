using System;
using System.Linq;
using OpenQA.Selenium;
using TALXIS.TestKit.Selectors.DTO.Locators;
using TALXIS.TestKit.Selectors.WebClientManagement.Helpers;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors.WebClientManagement
{
    public class TimelineManager
    {
        public WebClient Client { get; }


        public TimelineManager(WebClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        internal BrowserCommandResult<bool> Assign(string userOrTeamToAssign, int thinkTime = Constants.DefaultThinkTime)
        {
            //Click the Assign Button on the Entity Record
            Client.ThinkTime(thinkTime);

            return Client.Execute(Client.GetOptions($"Assign Entity"), driver =>
            {
                var assignBtn = driver.WaitUntilAvailable(EntityElementsLocators.Assign,
                    "Assign Button is not available");

                assignBtn?.Click();
                DialogHelper.AssignDialog(Dialogs.AssignTo.User, Client, userOrTeamToAssign);

                return true;
            });
        }

        /// <summary>
        /// Provided a By object which represents a HTML Button object, this method will
        /// find it and click it.
        /// </summary>
        /// <param name="by">The object of Type By which represents a HTML Button object</param>
        /// <returns>True on success, False/Exception on failure to invoke any action</returns>
        internal BrowserCommandResult<bool> ClickButton(By by)
        {
            return Client.Execute($"Open Timeline Add Post Popout", driver =>
            {
                var button = driver.WaitUntilAvailable(by);
                if (button.TagName.Equals("button"))
                {
                    try
                    {
                        driver.ClickWhenAvailable(by);
                    }
                    catch
                    {
                        // Element is stale reference is thrown here since the HTML components 
                        // get destroyed and thus leaving the references null. 
                        // It is expected that the components will be destroyed and the next 
                        // action should take place after it and hence it is ignored.
                    }

                    return true;
                }
                else if (button.FindElements(By.TagName("button")).Any())
                {
                    button.FindElements(By.TagName("button")).First().Click();
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Control does not exist");
                }
            });
        }

        /// <summary>
        /// Provided a fieldname as a XPath which represents a HTML Button object, this method will
        /// find it and click it.
        /// </summary>
        /// <param name="fieldNameXpath">The field as a XPath which represents a HTML Button object</param>
        /// <returns>True on success, Exception on failure to invoke any action</returns>
        internal BrowserCommandResult<bool> ClickButton(string fieldNameXpath)
        {
            try
            {
                return ClickButton(By.XPath(fieldNameXpath));
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Field: {fieldNameXpath} with Does not exist", e);
            }
        }

        internal void ClickTab(IWebElement tabList, string xpath, string name)
                    => ActionHelper.ClickTab(tabList, xpath, name, Client);

        internal BrowserCommandResult<bool> CloseOpportunity(double revenue, DateTime closeDate, string description, int thinkTime = Constants.DefaultThinkTime)
                    => CloseHelper.CloseOpportunity(revenue, closeDate, description, Client, thinkTime);

        internal BrowserCommandResult<bool> Delete(int thinkTime = Constants.DefaultThinkTime)
        {
            Client.ThinkTime(thinkTime);

            return Client.Execute(Client.GetOptions($"Delete Entity"), driver =>
            {
                var deleteBtn = driver.WaitUntilAvailable(EntityElementsLocators.Delete,
                    "Delete Button is not available");

                deleteBtn?.Click();
                DialogHelper.ConfirmationDialog(true, Client);

                driver.WaitForTransaction();

                return true;
            });
        }

        /// <summary>
        /// This method opens the popout menus in the Dynamics 365 pages. 
        /// This method uses a thinktime since after the page loads, it takes some time for the 
        /// widgets to load before the method can find and popout the menu.
        /// </summary>
        /// <param name="popoutName">The By Object of the Popout menu</param>
        /// <param name="popoutItemName">The By Object of the Popout Item name in the popout menu</param>
        /// <param name="thinkTime">Amount of time(milliseconds) to wait before this method will click on the "+" popout menu.</param>
        /// <returns>True on success, False on failure to invoke any action</returns>
        internal BrowserCommandResult<bool> OpenAndClickPopoutMenu(By menuName, By menuItemName, int thinkTime = Constants.DefaultThinkTime)
        {
            Client.ThinkTime(thinkTime);

            return Client.Execute($"Open menu", driver =>
            {
                driver.ClickWhenAvailable(menuName);
                try
                {
                    driver.ClickWhenAvailable(menuItemName);
                }
                catch
                {
                    // Element is stale reference is thrown here since the HTML components 
                    // get destroyed and thus leaving the references null. 
                    // It is expected that the components will be destroyed and the next 
                    // action should take place after it and hence it is ignored.
                    return false;
                }

                return true;
            });
        }
        /// <summary>
        /// This method opens the popout menus in the Dynamics 365 pages. 
        /// This method uses a thinktime since after the page loads, it takes some time for the 
        /// widgets to load before the method can find and popout the menu.
        /// </summary>
        /// <param name="popoutName">The name of the Popout menu</param>
        /// <param name="popoutItemName">The name of the Popout Item name in the popout menu</param>
        /// <param name="thinkTime">Amount of time(milliseconds) to wait before this method will click on the "+" popout menu.</param>
        /// <returns>True on success, False on failure to invoke any action</returns>
        internal BrowserCommandResult<bool> OpenAndClickPopoutMenu(string popoutName, string popoutItemName, int thinkTime = Constants.DefaultThinkTime)
        {
            return this.OpenAndClickPopoutMenu(By.XPath(Elements.Xpath[popoutName]), By.XPath(Elements.Xpath[popoutItemName]), thinkTime);
        }

        /// <summary>
        /// Generic method to help click on any item which is clickable or uniquely discoverable with a By object.
        /// </summary>
        /// <param name="by">The xpath of the HTML item as a By object</param>
        /// <returns>True on success, Exception on failure to invoke any action</returns>
        internal BrowserCommandResult<bool> SelectTab(string tabName, string subTabName = "", int thinkTime = Constants.DefaultThinkTime)
            => ActionHelper.SelectTab(tabName, Client, subTabName, thinkTime);

        /// <summary>
        /// A generic setter method which will find the HTML Textbox/Textarea object and populate
        /// it with the value provided. The expected tag name is to make sure that it hits
        /// the expected tag and not some other object with the similar fieldname.
        /// </summary>
        /// <param name="fieldName">The name of the field representing the HTML Textbox/Textarea object</param>
        /// <param name="value">The string value which will be populated in the HTML Textbox/Textarea</param>
        /// <param name="expectedTagName">Expected values - textbox/textarea</param>
        /// <returns>True on success, Exception on failure to invoke any action</returns>
        internal BrowserCommandResult<bool> SetValue(string fieldName, string value, string expectedTagName)
        {
            return Client.Execute($"SetValue (Generic)", driver =>
            {
                var inputbox = driver.WaitUntilAvailable(By.XPath(Elements.Xpath[fieldName]));
                if (expectedTagName.Equals(inputbox.TagName, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!inputbox.TagName.Contains("iframe", StringComparison.InvariantCultureIgnoreCase))
                    {
                        inputbox.Click(true);
                        inputbox.Clear();
                        inputbox.SendKeys(value);
                    }
                    else
                    {
                        driver.SwitchTo().Frame(inputbox);

                        driver.WaitUntilAvailable(By.TagName("iframe"));
                        driver.SwitchTo().Frame(0);

                        var inputBoxBody = driver.WaitUntilAvailable(By.TagName("body"));
                        inputBoxBody.Click(true);
                        inputBoxBody.SendKeys(value);

                        driver.SwitchTo().DefaultContent();
                    }

                    return true;
                }

                throw new InvalidOperationException($"Field: {fieldName} with tagname {expectedTagName} Does not exist");
            });
        }

        internal BrowserCommandResult<bool> SwitchProcess(string processToSwitchTo, int thinkTime = Constants.DefaultThinkTime)
        {
            Client.ThinkTime(thinkTime);

            return Client.Execute(Client.GetOptions($"Switch BusinessProcessFlow"), driver =>
            {
                driver.ClickWhenAvailable(EntityElementsLocators.ProcessButton, TimeSpan.FromSeconds(5));

                driver.ClickWhenAvailable(
                    EntityElementsLocators.SwitchProcess,
                    TimeSpan.FromSeconds(5),
                    "The Switch Process Button is not available."
                );

                return true;
            });
        }

        internal void ClickCreateOnTimeline(string activityName)
        {
            var _ = Client.Execute(Client.GetOptions($"Click Create for '{activityName}' on the Timeline"), driver =>
            {
                driver.ClickWhenAvailable(By.XPath(Elements.Xpath[Reference.Timeline.Popout]));
                driver.ClickWhenAvailable(By.XPath($"//li[contains(@id,\"notescontrol-createNewRecord_flyoutMenuItem_{activityName}\")]"));
                return true;
            }).Value;
        }
    }
}
