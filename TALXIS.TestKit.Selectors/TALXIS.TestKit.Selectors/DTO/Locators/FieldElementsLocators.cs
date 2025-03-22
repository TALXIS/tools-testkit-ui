using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class FieldElementsLocators
    {
        internal static By ReadOnly => By.XPath(AppElements.Xpath[AppReference.Field.ReadOnly]);
        internal static By Required => By.XPath(AppElements.Xpath[AppReference.Field.Required]);
        internal static By RequiredIcon => By.XPath(AppElements.Xpath[AppReference.Field.RequiredIcon]);
        internal static By IndexSearch => By.XPath(AppElements.Xpath[AppReference.Field.IndexSearch]);

        internal static By LableSearch(string lable) => By.XPath(AppElements.Xpath[AppReference.Field.LableSearch].Replace("[LABLE]", lable));
    }
}

