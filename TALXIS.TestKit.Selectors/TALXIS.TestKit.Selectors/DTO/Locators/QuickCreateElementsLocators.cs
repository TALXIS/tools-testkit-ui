using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class QuickCreateElementsLocators
    {
        internal static By QuickCreateFormContext => By.XPath(AppElements.Xpath[AppReference.QuickCreate.QuickCreateFormContext]);
        internal static By SaveButton => By.XPath(AppElements.Xpath[AppReference.QuickCreate.SaveButton]);
        internal static By SaveAndCloseButton => By.XPath(AppElements.Xpath[AppReference.QuickCreate.SaveAndCloseButton]);
        internal static By CancelButton => By.XPath(AppElements.Xpath[AppReference.QuickCreate.CancelButton]);
    }
}
