using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class AdvancedLookupElementsLocators
    {
        internal static By Container => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.Container]);
        internal static By SearchInput => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.SearchInput]);
        internal static By ResultRows => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.ResultRows]);
        internal static By FilterTables => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.FilterTables]);
        internal static By FilterTable(string name) => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.FilterTable].Replace("[NAME]", name));
        internal static By AddNewTables => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.AddNewTables]);
        internal static By DoneButton => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.DoneButton]);
        internal static By AddNewRecordButton => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.AddNewRecordButton]);
        internal static By AddNewButton => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.AddNewButton]);
        internal static By ViewSelectorCaret => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.ViewSelectorCaret]);
        internal static By ViewDropdownList => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.ViewDropdownList]);
        internal static By ViewDropdownListItem(string name) => By.XPath(AppElements.Xpath[AppReference.AdvancedLookup.ViewDropdownListItem].Replace("[NAME]", name));
    }
}
