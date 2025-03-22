using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TALXIS.TestKit.Selectors.DTO.Locators;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors.WebClientManagement
{
    public class GlobalSearchManager
    {
        public WebClient Client { get; }

        public GlobalSearchManager(WebClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Filter by group and value in the Global Search Results.
        /// </summary>
        /// <param name="filterby">The Group that contains the filter you want to use.</param>
        /// <param name="value">The value listed in the group by area.</param>
        /// <example>xrmBrowser.GlobalSearch.Filter("Record Type", "Accounts");</example>
        public BrowserCommandResult<bool> Filter(string filterBy, string value, int thinkTime = Constants.DefaultThinkTime)
        {
            Client.ThinkTime(thinkTime);

            return Client.Execute(Client.GetOptions($"Filter With: {value}"), driver =>
            {
                driver.WaitForTransaction();

                if (driver.TryFindElement(GlobalSearchElementsLocators.GroupContainer( filterBy), out var entityPicker))
                {
                    // Categorized Search
                    entityPicker.ClickWhenAvailable(
                    GlobalSearchElementsLocators.FilterValue( value),
                    $"Filter By Value '{value}' does not exist in the Filter options.");
                }
                else if (filterBy == "Record Type" && driver.TryFindElement(GlobalSearchElementsLocators.RelevanceSearchResultsTab( value), out var entityTab))
                {
                    // Relevance Search
                    entityTab.Click();
                }
                else
                {
                    throw new InvalidOperationException("Unable to filter global search results.");
                }

                driver.WaitForTransaction();

                return true;
            });
        }

        /// <summary>
        /// Filter by entity in the Global Search Results.
        /// </summary>
        /// <param name="entity">The entity you want to filter with.</param>
        /// <param name="thinkTime">Used to simulate a wait time between human interactions. The Default is 2 seconds.</param>
        /// <example>xrmBrowser.GlobalSearch.FilterWith("Account");</example>
        public BrowserCommandResult<bool> FilterWith(string entity, int thinkTime = Constants.DefaultThinkTime)
        {
            Client.ThinkTime(thinkTime);

            return Client.Execute(Client.GetOptions($"Filter With: {entity}"), driver =>
            {
                driver.WaitForTransaction();

                if (driver.TryFindElement(GlobalSearchElementsLocators.Filter, out var filter))
                {
                    // Categorized Search
                    var option = filter
                        .FindElements(By.TagName("option"))
                        .FirstOrDefault(x => x.Text == entity);

                    if (option == null)
                    {
                        throw new InvalidOperationException($"Entity '{entity}' does not exist in the Filter options.");
                    }

                    filter.Click();
                    option.Click();
                }
                else if (driver.TryFindElement(GlobalSearchElementsLocators.RelevanceSearchResultsTab( entity), out var entityTab))
                {
                    // Relevance Search
                    entityTab.Click();
                }
                else
                {
                    throw new InvalidOperationException($"Unable to filter global search results by the '{entity}' table.");
                }

                return true;
            });
        }

        /// <summary>
        /// Opens the specified record in the Global Search Results.
        /// </summary>
        /// <param name="entity">The entity you want to open a record.</param>
        /// <param name="index">The index of the record you want to open.</param>
        /// <param name="thinkTime">Used to simulate a wait time between human interactions. The Default is 2 seconds.</param> time.</param>
        /// <example>xrmBrowser.GlobalSearch.OpenRecord("Accounts",0);</example>
        public BrowserCommandResult<bool> OpenGlobalSearchRecord(string entity, int index, int thinkTime = Constants.DefaultThinkTime)
        {
            Client.ThinkTime(thinkTime);

            return Client.Execute(Client.GetOptions($"Open Global Search Record"), driver =>
            {
                driver.WaitForTransaction();

                if (driver.TryFindElement(GlobalSearchElementsLocators.CategorizedResultsContainer, out var categorizedContainer))
                {
                    // Categorized Search
                    var records = categorizedContainer.FindElements(GlobalSearchElementsLocators.CategorizedResults( entity));
                    if (index >= records.Count)
                    {
                        throw new InvalidOperationException($"There was less than {index + 1} records in the search result.");
                    }

                    records[index].Click(true);
                }
                else if (driver.TryFindElement(By.Id("resultsContainer-view"), out var relevanceContainer))
                {
                    // Relevance Search
                    var selectedTab = relevanceContainer.FindElement(GlobalSearchElementsLocators.RelevanceSearchResultsSelectedTab);
                    if (selectedTab.GetAttribute("name") != entity)
                    {
                        this.FilterWith(entity);
                    }

                    var links = relevanceContainer.FindElements(GlobalSearchElementsLocators.RelevanceSearchResultLinks);
                    if (index >= links.Count)
                    {
                        throw new InvalidOperationException($"There was less than {index + 1} records in the search result.");
                    }

                    new Actions(driver).DoubleClick(links[index]).Perform();
                }
                else
                {
                    throw new NotFoundException("Unable to locate global search results.");
                }

                driver.WaitForTransaction();

                return true;
            });
        }

        /// <summary>
        /// Searches for the specified criteria in Global Search.
        /// </summary>
        /// <param name="criteria">Search criteria.</param>
        /// <param name="thinkTime">Used to simulate a wait time between human interactions. The Default is 2 seconds.</param> time.</param>
        /// <example>xrmBrowser.GlobalSearch.Search("Contoso");</example>
        internal BrowserCommandResult<bool> GlobalSearch(string criteria, int thinkTime = Constants.DefaultThinkTime)
        {
            Client.ThinkTime(thinkTime);

            return Client.Execute(Client.GetOptions($"Global Search: {criteria}"), driver =>
            {
                driver.WaitForTransaction();

                IWebElement input;
                if (driver.TryFindElement(By.Id("GlobalSearchBox"), out var globalSearch))
                {
                    input = globalSearch;
                }
                else
                {
                    driver.ClickWhenAvailable(
                        By.XPath(AppElements.Xpath[AppReference.Navigation.SearchButton]),
                        2.Seconds(),
                        "The Global Search (Categorized Search) button is not available.");

                    input = driver.WaitUntilVisible(
                        GlobalSearchElementsLocators.Text,
                        2.Seconds(),
                        "The Categorized Search text field is not available.");
                }

                input.Click();
                input.SendKeys(criteria, true);
                input.SendKeys(Keys.Enter);

                driver.WaitForTransaction();

                return true;
            });
        }
    }
}
