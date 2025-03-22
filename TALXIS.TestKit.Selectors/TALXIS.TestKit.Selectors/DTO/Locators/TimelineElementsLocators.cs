using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class TimelineElementsLocators
    {
        internal static By SaveAndClose(string name) => By.XPath(AppElements.Xpath[AppReference.Timeline.SaveAndClose].Replace("[NAME]", name));

    }
}
