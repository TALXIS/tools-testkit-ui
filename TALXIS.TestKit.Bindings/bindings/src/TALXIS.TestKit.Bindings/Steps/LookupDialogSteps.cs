namespace TALXIS.TestKit.Bindings.Steps
{
    using OpenQA.Selenium;
    using Reqnroll;
    using TALXIS.TestKit.Selectors.Browser;

    /// <summary>
    /// Step bindings related to lookup dialogs.
    /// </summary>
    [Binding]
    public class LookupDialogSteps : PowerAppsStepDefiner
    {
        /// <summary>
        /// Searches and Selects the first matching result.
        /// </summary>
        /// <param name="searchTerm">The term to search for.</param>
        [When("I select '([^']+)' in the lookup dialog")]
        public static void WhenISelectInTheLookupDialog(string searchTerm)
        {
            XrmApp.Lookup.SelectLookupResult(searchTerm);
        }

        /// <summary>
        /// Clicks the Add button.
        /// </summary>
        [When("I click Add in the lookup dialog")]
        public static void WhenIClickAddInTheLookupDialog()
        {
            XrmApp.Lookup.ClickAddButton();
        }
    }
}
