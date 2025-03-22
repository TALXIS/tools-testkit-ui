using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class LookupElementsLocators
    {
        internal static By RelatedEntityLabel(string name) => By.XPath(AppElements.Xpath[AppReference.Lookup.RelatedEntityLabel].Replace("[NAME]", name));
        internal static By AdvancedLookupButton => By.XPath(AppElements.Xpath[AppReference.Lookup.AdvancedLookupButton]);
        internal static By ViewRows => By.XPath(AppElements.Xpath[AppReference.Lookup.ViewRows]);
        internal static By LookupResultRows => By.XPath(AppElements.Xpath[AppReference.Lookup.LookupResultRows]);
        internal static By NewButton => By.XPath(AppElements.Xpath[AppReference.Lookup.NewButton]);
        internal static By RecordList => By.XPath(AppElements.Xpath[AppReference.Lookup.RecordList]);
    }
}
