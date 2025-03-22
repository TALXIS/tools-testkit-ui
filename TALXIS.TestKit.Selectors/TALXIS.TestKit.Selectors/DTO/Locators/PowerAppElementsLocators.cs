using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class PowerAppElementsLocators
    {
        internal static By ModelFormContainer(string name) => By.XPath(AppElements.Xpath[AppReference.PowerApp.ModelFormContainer].Replace("[NAME]", name));
        internal static By Control(string name) => By.XPath(AppElements.Xpath[AppReference.PowerApp.Control].Replace("[NAME]", name));
    }
}
