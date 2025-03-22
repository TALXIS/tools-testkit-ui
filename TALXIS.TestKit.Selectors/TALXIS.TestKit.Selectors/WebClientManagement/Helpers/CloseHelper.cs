using System;
using OpenQA.Selenium;
using TALXIS.TestKit.Selectors.DTO.Locators;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors.WebClientManagement.Helpers
{
    internal class CloseHelper
    {
        internal static BrowserCommandResult<bool> CloseOpportunity(double revenue, DateTime closeDate, string description, WebClient client, int thinkTime = Constants.DefaultThinkTime)
        {
            client.ThinkTime(thinkTime);

            return client.Execute(client.GetOptions($"Close Opportunity"), driver =>
            {
                //SetValue(Elements.ElementId[AppReference.Dialogs.CloseOpportunity.ActualRevenueId], revenue.ToString(CultureInfo.CurrentCulture));
                //SetValue(Elements.ElementId[AppReference.Dialogs.CloseOpportunity.CloseDateId], closeDate);
                //SetValue(Elements.ElementId[AppReference.Dialogs.CloseOpportunity.DescriptionId], description);

                driver.ClickWhenAvailable(DialogsElementsLocators.CloseOpportunity.Ok,
                    TimeSpan.FromSeconds(5),
                    "The Close Opportunity dialog is not available."
                );

                return true;
            });
        }

        internal static BrowserCommandResult<bool> CloseOpportunity(bool closeAsWon, WebClient client, int thinkTime = Constants.DefaultThinkTime)
        {
            client.ThinkTime(thinkTime);

            var xPathQuery = closeAsWon
                ? AppElements.Xpath[AppReference.Entity.CloseOpportunityWin]
                : AppElements.Xpath[AppReference.Entity.CloseOpportunityLoss];

            return client.Execute(client.GetOptions($"Close Opportunity"), driver =>
            {
                var closeBtn = driver.WaitUntilAvailable(By.XPath(xPathQuery), "Opportunity Close Button is not available");

                closeBtn?.Click();
                driver.WaitUntilVisible(DialogsElementsLocators.CloseOpportunity.Ok);
                CloseOpportunityDialog(true, client);

                return true;
            });
        }

        internal static BrowserCommandResult<bool> CloseOpportunityDialog(bool clickOK, WebClient client)
        {
            return client.Execute(client.GetOptions($"Close Opportunity Dialog"), driver =>
            {
                var inlineDialog = DialogHelper.SwitchToDialog(client);

                if (inlineDialog)
                {
                    //Close Opportunity
                    var xPath = AppElements.Xpath[AppReference.Dialogs.CloseOpportunity.Ok];

                    //Cancel
                    if (!clickOK)
                        xPath = AppElements.Xpath[AppReference.Dialogs.CloseOpportunity.Ok];

                    driver.ClickWhenAvailable(By.XPath(xPath), TimeSpan.FromSeconds(5), "The Close Opportunity dialog is not available.");
                }

                return true;
            });
        }
    }
}
