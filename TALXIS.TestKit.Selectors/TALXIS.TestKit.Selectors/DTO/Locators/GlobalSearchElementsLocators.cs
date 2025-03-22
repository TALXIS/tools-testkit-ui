using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class GlobalSearchElementsLocators
    {
        internal static By CategorizedSearchButton => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.CategorizedSearchButton]);
        internal static By RelevanceSearchButton => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.RelevanceSearchButton]);
        internal static By Text => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.Text]);
        internal static By Filter => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.Filter]);
        internal static By Container => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.Container]);
        internal static By EntityContainer(string name) => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.EntityContainer].Replace("[NAME]", name));
        internal static By Records => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.Records]);
        internal static By GroupContainer(string name) => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.GroupContainer].Replace("[NAME]", name));
        internal static By FilterValue(string name) => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.FilterValue].Replace("[NAME]", name));
        internal static By CategorizedResultsContainer => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.CategorizedResultsContainer]);
        internal static By CategorizedResults(string entity) => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.CategorizedResults].Replace("[ENTITY]", entity));
        internal static By RelevanceSearchResultsSelectedTab => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.RelevanceSearchResultsSelectedTab]);
        internal static By RelevanceSearchResultsTab(string name) => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.RelevanceSearchResultsTab].Replace("[NAME]", name));
        internal static By RelevanceSearchResultLinks => By.XPath(AppElements.Xpath[AppReference.GlobalSearch.RelevanceSearchResultLinks]);
    }
}
