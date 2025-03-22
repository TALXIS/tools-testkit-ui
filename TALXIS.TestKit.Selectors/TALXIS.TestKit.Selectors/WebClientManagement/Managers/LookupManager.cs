using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TALXIS.TestKit.Selectors.DTO;
using TALXIS.TestKit.Selectors.DTO.Locators;
using TALXIS.TestKit.Selectors.WebClientManagement.Helpers;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors.WebClientManagement
{
    public class LookupManager
    {
        public WebClient Client { get; }

        public LookupManager(WebClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Searches for a term in the lookup dialog and selects the first matching result.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        public void SelectLookupResult(string searchTerm)
        {
            Client.Execute<object>(Client.GetOptions("Select Lookup Record"), driver =>
            {
                // Wait until the lookup dialog container is available and find the lookup input field.
                var fieldContainer = driver.WaitUntilAvailable(
                    By.CssSelector("div[id=\"lookupDialogContainer\"] div[id=\"lookupDialogLookup\"]"));
                var input = fieldContainer.FindElement(By.TagName("input"));

                // Click the input, enter the search term, and submit with Enter key.
                input.Click();
                input.SendKeys(searchTerm);
                input.SendKeys(Keys.Enter);

                // Wait for the lookup results to appear and click the first result.
                fieldContainer.WaitUntilAvailable(By.CssSelector("li[data-id*=\"LookupResultsPopup\"]")).Click();

                return null;
            });
        }

        /// <summary>
        /// Clicks the Add button in the lookup dialog.
        /// </summary>
        public void ClickAddButton()
        {
            Client.Execute<object>(Client.GetOptions("Select Lookup Record"), driver =>
            {
                // Wait until the footer container of the lookup dialog is available.
                var container = driver.WaitUntilAvailable(
                    By.CssSelector("div[id=\"lookupDialogFooterContainer\"]"));
                // Find and click the Add (save) button.
                container.FindElement(By.CssSelector("button[data-id*=\"lookupDialogSaveBtn\"]")).Click();

                return null;
            });
        }


        internal BrowserCommandResult<bool> OpenLookupRecord(int index)
        {
            return this.Client.Execute(Client.GetOptions("Select Lookup Record"), driver =>
            {
                driver.WaitForTransaction();

                ReadOnlyCollection<IWebElement> rows = null;
                if (driver.TryFindElement(AdvancedLookupElementsLocators.Container, out var advancedLookup))
                {
                    // Advanced Lookup
                    rows = driver.FindElements(AdvancedLookupElementsLocators.ResultRows);
                }
                else
                {
                    // Lookup
                    rows = driver.FindElements(LookupElementsLocators.LookupResultRows);
                }

                if (rows == null || !rows.Any())
                {
                    throw new NotFoundException("No rows found");
                }

                var row = rows.ElementAt(index);

                if (advancedLookup == null)
                {
                    row.Click();
                }
                else
                {
                    if (!row.GetAttribute<bool?>("aria-selected").GetValueOrDefault())
                    {
                        row.Click();
                    }

                    advancedLookup.FindElement(AdvancedLookupElementsLocators.DoneButton).Click();
                }

                driver.WaitForTransaction();

                return true;
            });
        }

        internal static void TryRemoveLookupValue(IWebDriver driver, IWebElement fieldContainer, LookupItem control, bool removeAll = true, bool isHeader = false)
        {
            var controlName = control.Name;
            fieldContainer.Hover(driver);

            var xpathDeleteExistingValues = EntityElementsLocators.LookupFieldDeleteExistingValue( controlName);
            var existingValues = fieldContainer.FindElements(xpathDeleteExistingValues);

            var xpathToExpandButton = EntityElementsLocators.LookupFieldExpandCollapseButton( controlName);
            bool success = fieldContainer.TryFindElement(xpathToExpandButton, out var expandButton);
            if (success)
            {
                expandButton.Click(true);

                var count = existingValues.Count;
                fieldContainer.WaitUntil(x => x.FindElements(xpathDeleteExistingValues).Count > count);
            }

            fieldContainer.WaitUntilAvailable(EntityElementsLocators.TextFieldLookupSearchButton( controlName));

            existingValues = fieldContainer.FindElements(xpathDeleteExistingValues);
            if (existingValues.Count == 0)
                return;

            if (removeAll)
            {
                // Removes all selected items

                while (existingValues.Count > 0)
                {
                    foreach (var v in existingValues)
                        v.Click(true);

                    existingValues = fieldContainer.FindElements(xpathDeleteExistingValues);
                }

                return;
            }

            // Removes an individual item by value or index
            var value = control.Value;
            if (value == null)
                throw new InvalidOperationException($"No value or index has been provided for the LookupItem {controlName}. Please provide an value or an empty string or an index and try again.");

            if (value == string.Empty)
            {
                var index = control.Index;
                if (index >= existingValues.Count)
                    throw new InvalidOperationException($"Field '{controlName}' does not contain {index + 1} records. Please provide an index value less than {existingValues.Count}");

                existingValues[index].Click(true);
                return;
            }

            var existingValue = existingValues.FirstOrDefault(v => v.GetAttribute("aria-label").EndsWith(value));
            if (existingValue == null)
                throw new InvalidOperationException($"Field '{controlName}' does not contain a record with the name:  {value}");

            existingValue.Click(true);
            driver.WaitForTransaction();
        }

        internal BrowserCommandResult<bool> SearchLookupField(LookupItem control, string searchCriteria)
        {
            return this.Client.Execute(Client.GetOptions("Search Lookup Record"), driver =>
            {
                driver.WaitForTransaction();

                if (driver.TryFindElement(AdvancedLookupElementsLocators.Container, out var advancedLookup))
                {
                    // Advanced lookup
                    var search = advancedLookup.FindElement(AdvancedLookupElementsLocators.SearchInput);
                    search.Click();
                    search.SendKeys(Keys.Control + "a");
                    search.SendKeys(Keys.Backspace);
                    search.SendKeys(searchCriteria);

                    driver.WaitForTransaction();

                    OpenLookupRecord(0);
                }
                else
                {
                    // Lookup
                    control.Value = searchCriteria;
                    SetValueHelper.SetLookUp(Client,control, FormContextType.Entity);
                }

                driver.WaitForTransaction();

                return true;
            });
        }

        internal BrowserCommandResult<bool> SelectLookupAdvancedLookupButton()
        {
            return this.Client.Execute(Client.GetOptions("Click Advanced Lookup Button"), driver =>
            {
                driver.ClickWhenAvailable(
                    By.XPath(AppElements.Xpath[AppReference.Lookup.AdvancedLookupButton]),
                    10.Seconds(),
                    "The 'Advanced Lookup' button was not found. Ensure a search has been performed in the lookup first.");

                driver.WaitForTransaction();

                return true;
            });
        }

        internal BrowserCommandResult<bool> SelectLookupNewButton()
        {
            return this.Client.Execute(Client.GetOptions("Click New Lookup Button"), driver =>
            {
                driver.WaitForTransaction();

                if (driver.TryFindElement(AdvancedLookupElementsLocators.Container, out var advancedLookup))
                {
                    // Advanced lookup
                    if (advancedLookup.TryFindElement(AdvancedLookupElementsLocators.AddNewRecordButton, out var addNewRecordButton))
                    {
                        // Single table lookup
                        addNewRecordButton.Click();
                    }
                    else if (advancedLookup.TryFindElement(AdvancedLookupElementsLocators.AddNewButton, out var addNewButton))
                    {
                        // Composite lookup
                        var filterTables = advancedLookup.FindElements(AdvancedLookupElementsLocators.FilterTables).ToList();
                        var tableIndex = filterTables.FindIndex(t => t.HasAttribute("aria-current"));

                        addNewButton.Click();
                        driver.WaitForTransaction();

                        var addNewTables = advancedLookup.FindElements(AdvancedLookupElementsLocators.AddNewTables);
                        addNewTables.ElementAt(tableIndex).Click();
                    }
                }
                else
                {
                    // Lookup
                    if (driver.HasElement(By.XPath(AppElements.Xpath[AppReference.Lookup.NewButton])))
                    {
                        var newButton = driver.FindElement(LookupElementsLocators.NewButton);

                        if (newButton.GetAttribute("disabled") == null)
                            driver.FindElement(LookupElementsLocators.NewButton).Click();
                        else
                            throw new ElementNotInteractableException("New button is not enabled.  If this is a mulit-entity lookup, please use SelectRelatedEntity first.");
                    }
                    else
                        throw new NotFoundException("New button not found.");
                }

                driver.WaitForTransaction();

                return true;
            });
        }

        internal BrowserCommandResult<bool> SelectLookupRelatedEntity(string entityName)
        {
            // Click the Related Entity on the Lookup Flyout
            return this.Client.Execute(Client.GetOptions($"Select Lookup Related Entity {entityName}"), driver =>
            {
                driver.WaitForTransaction();

                IWebElement relatedEntity = null;
                if (driver.TryFindElement(AdvancedLookupElementsLocators.Container, out var advancedLookup))
                {
                    // Advanced lookup
                    relatedEntity = advancedLookup.WaitUntilAvailable(
                        By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.FilterTable].Replace("[NAME]", entityName)),
                        2.Seconds());
                }
                else
                {
                    // Lookup 
                    relatedEntity = driver.WaitUntilAvailable(
                        By.XPath(AppElements.Xpath[AppReference.Lookup.RelatedEntityLabel].Replace("[NAME]", entityName)),
                        2.Seconds());
                }

                if (relatedEntity == null)
                {
                    throw new NotFoundException($"Lookup Entity {entityName} not found.");
                }

                relatedEntity.Click();
                driver.WaitForTransaction();

                return true;
            });
        }

        internal BrowserCommandResult<bool> SwitchLookupView(string viewName)
        {
            return Client.Execute(Client.GetOptions($"Select Lookup View {viewName}"), driver =>
            {
                var advancedLookup = driver.WaitUntilAvailable(
                    AdvancedLookupElementsLocators.Container,
                    2.Seconds());

                if (advancedLookup == null)
                {
                    SelectLookupAdvancedLookupButton();
                    advancedLookup = driver.WaitUntilAvailable(
                        AdvancedLookupElementsLocators.Container,
                        2.Seconds(),
                        "Expected Advanced Lookup dialog but it was not found.");
                }

                advancedLookup
                    .FindElement(AdvancedLookupElementsLocators.ViewSelectorCaret)
                    .Click();

                driver
                    .WaitUntilAvailable(AdvancedLookupElementsLocators.ViewDropdownList)
                    .ClickWhenAvailable(
                     By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.ViewDropdownListItem].Replace("[NAME]", viewName)),
                     2.Seconds(),
                     $"The '{viewName}' view isn't in the list of available lookup views.");

                driver.WaitForTransaction();

                return true;
            });
        }

        internal void SearchInLookup(string searchCriteria, LookupItem control)
        {
            Client.Execute<object>(Client.GetOptions($"Search In Lookup {searchCriteria}"), driver =>
            {
                // Wait until the lookup container is available and find the input field.
                var fieldContainer = driver.WaitUntilAvailable(
                    By.XPath(AppElements.Xpath[AppReference.Entity.TextFieldContainer].Replace("[NAME]", control.Name)));
                var input = fieldContainer.FindElement(By.TagName("input"));

                // Click the input, enter the search criteria, and submit with Enter key.
                input.Click();
                input.SendKeys(searchCriteria);
                input.SendKeys(Keys.Enter);

                // Wait for the lookup transaction to complete.
                driver.WaitForTransaction();

                // Возвращаем значение, например, null.
                return null;
            });
        }

        internal IEnumerable<string> GetLookupRecordNames()
        {
            return Client.Execute<IEnumerable<string>>(
                Client.GetOptions("Get Lookup Record Names"),
                driver =>
                {
                    IWebElement flyout;
                    // Try to find the flyout element by one of two locators.
                    if (!driver.TryFindElement(By.CssSelector("[data-id*=SimpleLookupControlFlyout]"), out flyout))
                    {
                        if (!driver.TryFindElement(By.CssSelector("div[data-id*=LookupResultsDropdown][aria-label*=\"Lookup results\"]"), out flyout))
                        {
                            throw new ElementNotVisibleException("The flyout is not visible for the lookup.");
                        }
                    }

                    // Wait until at least one lookup record is available.
                    new WebDriverWait(driver, TimeSpan.FromSeconds(5))
                        .Until(d => d.FindElement(By.CssSelector("ul[aria-label=\"Lookup results\"] li")));

                    // Get the list items and extract the record names.
                    var items = driver
                        .FindElements(By.CssSelector("ul[aria-label=\"Lookup results\"] li"))
                        .Select(e => e.Text.Split(new[] { "\r\n" }, StringSplitOptions.None)[0]);

                    return items;
                }).Value;
        }

        internal void SelectRelatedLookupRecord(string lookupName)
        {
            Client.Execute<object>(Client.GetOptions($"Select Related Lookup Record"), driver =>
            {
                driver.WaitUntilAvailable(By.XPath(AppElements.Xpath[AppReference.Entity.LookupFieldExistingValue].Replace("[NAME]", lookupName))).Click();

                driver.WaitForTransaction();

                return null;
            });
        }

        internal void OpenAdvancedLookupDialog(string lookupField)
        {
            Client.Execute<object>(Client.GetOptions($"Select Related Lookup Record"), driver =>
            {
                var locator = By.CssSelector("button[data-id=\"[NAME].fieldControl-LookupResultsDropdown_[NAME]_advancedLookupBtnContainer\"]".Replace("[NAME]", lookupField));

                driver.ClickWhenAvailable(locator);

                return null;
            });
        }
    }
}
