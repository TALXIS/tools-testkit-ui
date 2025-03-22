using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class CustomerServiceCopilotElementsLocators
    {
        internal static By ControlId => By.XPath(AppElements.Xpath[AppReference.CustomerServiceCopilot.ControlId]);
        internal static By ControlButtonId => By.XPath(AppElements.Xpath[AppReference.CustomerServiceCopilot.ControlButtonId]);
        internal static By UserInput => By.XPath(AppElements.Xpath[AppReference.CustomerServiceCopilot.UserInput]);
        internal static By UserSubmit => By.XPath(AppElements.Xpath[AppReference.CustomerServiceCopilot.UserSubmit]);
    }
}
