using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class BusinessProcessFlowElementsLocators
    {
        internal static By NextStage_UCI => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.NextStage_UCI]);
        internal static By Flyout_UCI => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.Flyout_UCI]);
        internal static By NextStageButton => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.NextStageButton]);
        internal static By SetActiveButton => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.SetActiveButton]);
        internal static By BusinessProcessFlowFieldName(string name) => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.BusinessProcessFlowFieldName].Replace("[NAME]", name));
        internal static By BusinessProcessFlowFormContext => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.BusinessProcessFlowFormContext]);
        internal static By TextFieldContainer(string name) => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.TextFieldContainer].Replace("[NAME]", name));
        internal static By FieldSectionItemContainer => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.FieldSectionItemContainer]);
        internal static By TextFieldLabel(string name) => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.TextFieldLabel].Replace("[NAME]", name));
        internal static By BooleanFieldContainer(string name) => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.BooleanFieldContainer].Replace("[NAME]", name));
        internal static By BooleanFieldSelectedOption(string name) => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.BooleanFieldSelectedOption].Replace("[NAME]", name));
        internal static By DateTimeFieldContainer(string name) => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.DateTimeFieldContainer].Replace("[NAME]", name));
        internal static By FieldControlDateTimeInputUCI(string field) => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.FieldControlDateTimeInputUCI].Replace("[FIELD]", field));
        internal static By PinStageButton => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.PinStageButton]);
        internal static By CloseStageButton => By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.CloseStageButton]);
    }
}

