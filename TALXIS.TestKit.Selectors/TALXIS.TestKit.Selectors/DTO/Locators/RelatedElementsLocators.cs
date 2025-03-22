using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class RelatedElementsLocators
    {
        internal static By CommandBarButton(string name) => By.XPath(AppElements.Xpath[AppReference.Related.CommandBarButton].Replace("[NAME]", name));
        internal static By CommandBarSubButton(string name) => By.XPath(AppElements.Xpath[AppReference.Related.CommandBarSubButton].Replace("[NAME]", name));
        internal static By CommandBarOverflowContainer => By.XPath(AppElements.Xpath[AppReference.Related.CommandBarOverflowContainer]);
        internal static By CommandBarOverflowButton => By.XPath(AppElements.Xpath[AppReference.Related.CommandBarOverflowButton]);
        internal static By CommandBarButtonList => By.XPath(AppElements.Xpath[AppReference.Related.CommandBarButtonList]);
        internal static By CommandBarFlyoutButtonList => By.XPath(AppElements.Xpath[AppReference.Related.CommandBarFlyoutButtonList]);
    }
}
