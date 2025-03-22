using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class MultiSelectElementsLocators
    {
        internal static By DivContainer(string name) => By.XPath(AppElements.Xpath[AppReference.MultiSelect.DivContainer].Replace("[NAME]", name));
        internal static By InputSearch => By.XPath(AppElements.Xpath[AppReference.MultiSelect.InputSearch]);
        internal static By SelectedRecord => By.XPath(AppElements.Xpath[AppReference.MultiSelect.SelectedRecord]);
        internal static By SelectedRecordButton => By.XPath(AppElements.Xpath[AppReference.MultiSelect.SelectedRecordButton]);
        internal static By SelectedOptionDeleteButton => By.XPath(AppElements.Xpath[AppReference.MultiSelect.SelectedOptionDeleteButton]);
        internal static By SelectedRecordLabel => By.XPath(AppElements.Xpath[AppReference.MultiSelect.SelectedRecordLabel]);
        internal static By FlyoutCaret => By.XPath(AppElements.Xpath[AppReference.MultiSelect.FlyoutCaret]);
        internal static By FlyoutOption(string name) => By.XPath(AppElements.Xpath[AppReference.MultiSelect.FlyoutOption].Replace("[NAME]", name));
        internal static By FlyoutOptionCheckbox => By.XPath(AppElements.Xpath[AppReference.MultiSelect.FlyoutOptionCheckbox]);
        internal static By ExpandCollapseButton => By.XPath(AppElements.Xpath[AppReference.MultiSelect.ExpandCollapseButton]);
    }
}
