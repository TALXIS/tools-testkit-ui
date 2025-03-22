using System;
using OpenQA.Selenium;
using TALXIS.TestKit.Selectors.DTO.Locators;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors.WebClientManagement
{
    public class DashboardManager
    {
        public WebClient Client { get; }

        public DashboardManager(WebClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        internal BrowserCommandResult<bool> SelectDashboard(string dashboardName, int thinkTime = Constants.DefaultThinkTime)
        {
            Client.ThinkTime(thinkTime);

            return Client.Execute(Client.GetOptions($"Select Dashboard"), driver =>
            {
                //Click the drop-down arrow
                driver.ClickWhenAvailable(DashboardElementsLocators.DashboardSelector);
                //Select the dashboard
                driver.ClickWhenAvailable(DashboardElementsLocators.DashboardItemUCI(dashboardName));

                // Wait for Dashboard to load
                driver.WaitForTransaction();

                return true;
            });
        }
    }
}
