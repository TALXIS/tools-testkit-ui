using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class DashboardElementsLocators
    {
        internal static By DashboardSelector => By.XPath(AppElements.Xpath[AppReference.Dashboard.DashboardSelector]);
        internal static By DashboardItem(string name) => By.XPath(AppElements.Xpath[AppReference.Dashboard.DashboardItem].Replace("[NAME]", name));
        internal static By DashboardItemUCI(string name) => By.XPath(AppElements.Xpath[AppReference.Dashboard.DashboardItemUCI].Replace("[NAME]", name));
    }
}
