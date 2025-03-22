using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class PerformanceWidgetElementsLocators
    {
        internal static By Container => By.XPath(AppElements.Xpath[AppReference.PerformanceWidget.Container]);
        internal static By Page(string name) => By.XPath(AppElements.Xpath[AppReference.PerformanceWidget.Page].Replace("[NAME]", name));
    }
}
