using System;
using OpenQA.Selenium;
using TALXIS.TestKit.Selectors.DTO.Locators;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors.WebClientManagement
{
    public class BusinessProcessFlowManager
    {
        public WebClient Client { get; }

        public BusinessProcessFlowManager(WebClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Sets the value of a picklist.
        /// </summary>
        /// <param name="option">The option you want to set.</param>
        /// <example>xrmBrowser.BusinessProcessFlow.SetValue(new OptionSet { Name = "preferredcontactmethodcode", Value = "Email" });</example>
        public BrowserCommandResult<bool> BPFSetValue(OptionSet option)
        {
            return Client.Execute(Client.GetOptions($"Set BPF Value: {option.Name}"), driver =>
            {
                var fieldContainer = driver.WaitUntilAvailable(EntityElementsLocators.TextFieldContainer( option.Name));

                if (fieldContainer.FindElements(By.TagName("select")).Count > 0)
                {
                    var select = fieldContainer.FindElement(By.TagName("select"));
                    var options = select.FindElements(By.TagName("option"));

                    foreach (var op in options)
                    {
                        if (op.Text != option.Value && op.GetAttribute("value") != option.Value) continue;
                        op.Click(true);
                        break;
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Field: {option.Name} Does not exist");
                }

                return true;
            });
        }

        /// <summary>
        /// Sets the value of a Boolean Item.
        /// </summary>
        /// <param name="option">The option you want to set.</param>
        /// <example>xrmBrowser.BusinessProcessFlow.SetValue(new BooleanItem { Name = "preferredcontactmethodcode"});</example>
        public BrowserCommandResult<bool> BPFSetValue(BooleanItem option)
        {
            return Client.Execute(Client.GetOptions($"Set BPF Value: {option.Name}"), driver =>
            {
                var fieldContainer = driver.WaitUntilAvailable(BusinessProcessFlowElementsLocators.BooleanFieldContainer( option.Name));
                var existingValue = fieldContainer.GetAttribute("Title") == "Yes";

                if (option.Value != existingValue)
                {
                    fieldContainer.Click();
                    fieldContainer.ClickWhenAvailable(By.XPath("//option[not(@data-selected)]"));
                }

                driver.WaitForTransaction();

                return true;
            });
        }

        /// <summary>
        /// Sets the value of a Date Field.
        /// </summary>
        /// <param name="field">The field id or name.</param>
        /// <param name="date">DateTime value.</param>
        /// <param name="format">DateTime format</param>
        /// <example> xrmBrowser.BusinessProcessFlow.SetValue("birthdate", DateTime.Parse("11/1/1980"));</example>
        public BrowserCommandResult<bool> BPFSetValue(string field, DateTime date, string format = "MM dd yyyy")
        {
            return Client.Execute(Client.GetOptions($"Set BPF Value: {field}"), driver =>
            {
                var dateFieldXPath = BusinessProcessFlowElementsLocators.DateTimeFieldContainer( field);

                if (driver.HasElement(dateFieldXPath))
                {
                    var fieldElement = driver.ClickWhenAvailable(dateFieldXPath);

                    if (fieldElement.GetAttribute("value").Length > 0)
                    {
                        //fieldElement.Click();
                        //fieldElement.SendKeys(date.ToString(format));
                        //fieldElement.SendKeys(Keys.Enter);

                        fieldElement.Click();
                        Client.ThinkTime(250);
                        fieldElement.Click();
                        Client.ThinkTime(250);
                        fieldElement.SendKeys(Keys.Backspace);
                        Client.ThinkTime(250);
                        fieldElement.SendKeys(Keys.Backspace);
                        Client.ThinkTime(250);
                        fieldElement.SendKeys(Keys.Backspace);
                        Client.ThinkTime(250);
                        fieldElement.SendKeys(date.ToString(format), true);
                        Client.ThinkTime(500);
                        fieldElement.SendKeys(Keys.Tab);
                        Client.ThinkTime(250);
                    }
                    else
                    {
                        fieldElement.Click();
                        Client.ThinkTime(250);
                        fieldElement.Click();
                        Client.ThinkTime(250);
                        fieldElement.SendKeys(Keys.Backspace);
                        Client.ThinkTime(250);
                        fieldElement.SendKeys(Keys.Backspace);
                        Client.ThinkTime(250);
                        fieldElement.SendKeys(Keys.Backspace);
                        Client.ThinkTime(250);
                        fieldElement.SendKeys(date.ToString(format));
                        Client.ThinkTime(250);
                        fieldElement.SendKeys(Keys.Tab);
                        Client.ThinkTime(250);
                    }
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        internal BrowserCommandResult<bool> BPFClose(string stageName, int thinkTime = Constants.DefaultThinkTime)
        {
            return Client.Execute(Client.GetOptions($"Close BPF: {stageName}"), driver =>
            {
                //Click the BPF Stage
                SelectStage(stageName, 0);
                driver.WaitForTransaction();

                //Pin the Stage
                if (driver.HasElement(BusinessProcessFlowElementsLocators.CloseStageButton))
                    driver.FindElement(BusinessProcessFlowElementsLocators.CloseStageButton).Click(true);
                else
                    throw new NotFoundException($"Close button for stage {stageName} not found.");

                driver.WaitForTransaction();
                return true;
            });
        }

        internal BrowserCommandResult<Field> BPFGetField(string field)
        {
            return Client.Execute(Client.GetOptions($"Get Field"), driver =>
            {

                // Initialize the Business Process Flow context
                var formContext = driver.WaitUntilAvailable(BusinessProcessFlowElementsLocators.BusinessProcessFlowFormContext);
                var fieldElement = formContext.WaitUntilAvailable(By.XPath(AppElements.Xpath[AppReference.BusinessProcessFlow.FieldSectionItemContainer].Replace("[NAME]", field)));
                Field returnField = new Field(fieldElement);
                returnField.Name = field;

                IWebElement fieldLabel = null;

                fieldLabel = fieldElement.FindElement(BusinessProcessFlowElementsLocators.TextFieldLabel( field));

                if (fieldLabel != null)
                {
                    returnField.Label = fieldLabel.Text;
                }

                return returnField;
            });
        }

        internal BrowserCommandResult<bool> BPFPin(string stageName, int thinkTime = Constants.DefaultThinkTime)
        {
            return Client.Execute(Client.GetOptions($"Pin BPF: {stageName}"), driver =>
            {
                //Click the BPF Stage
                SelectStage(stageName, 0);
                driver.WaitForTransaction();

                //Pin the Stage
                if (driver.HasElement(BusinessProcessFlowElementsLocators.PinStageButton))
                    driver.FindElement(BusinessProcessFlowElementsLocators.PinStageButton).Click();
                else
                    throw new NotFoundException($"Pin button for stage {stageName} not found.");

                driver.WaitForTransaction();
                return true;
            });
        }

        /// <summary>
        /// Set Value
        /// </summary>
        /// <param name="field">The field</param>
        /// <param name="value">The value</param>
        /// <example>xrmApp.BusinessProcessFlow.SetValue("firstname", "Test");</example>
        internal BrowserCommandResult<bool> BPFSetValue(string field, string value)
        {
            return Client.Execute(Client.GetOptions($"Set BPF Value"), driver =>
            {
                var fieldContainer = driver.WaitUntilAvailable(BusinessProcessFlowElementsLocators.TextFieldContainer(field));

                if (fieldContainer.FindElements(By.TagName("input")).Count > 0)
                {
                    var input = fieldContainer.FindElement(By.TagName("input"));
                    if (input != null)
                    {
                        input.Click(true);
                        input.Clear();
                        input.SendKeys(value, true);
                        input.SendKeys(Keys.Tab);
                    }
                }
                else if (fieldContainer.FindElements(By.TagName("textarea")).Count > 0)
                {
                    var textarea = fieldContainer.FindElement(By.TagName("textarea"));
                    textarea.Click();
                    textarea.Clear();
                    textarea.SendKeys(value);
                }
                else
                {
                    throw new Exception($"Field with name {field} does not exist.");
                }

                return true;
            });
        }

        internal BrowserCommandResult<bool> NextStage(string stageName, Field businessProcessFlowField = null, int thinkTime = Constants.DefaultThinkTime)
        {
            Client.ThinkTime(thinkTime);

            return Client.Execute(Client.GetOptions($"Next Stage"), driver =>
            {
                //Find the Business Process Stages
                var processStages = driver.FindElements(BusinessProcessFlowElementsLocators.NextStage_UCI);

                if (processStages.Count == 0)
                    return true;

                foreach (var processStage in processStages)
                {
                    var divs = processStage.FindElements(By.TagName("div"));

                    //Click the Label of the Process Stage if found
                    foreach (var div in divs)
                    {
                        if (div.Text.Equals(stageName, StringComparison.OrdinalIgnoreCase))
                        {
                            div.Click();
                        }
                    }
                }

                var flyoutFooterControls = driver.FindElements(BusinessProcessFlowElementsLocators.Flyout_UCI);

                foreach (var control in flyoutFooterControls)
                {
                    //If there's a field to enter, fill it out
                    if (businessProcessFlowField != null)
                    {
                        var bpfField = control.FindElement(BusinessProcessFlowElementsLocators.BusinessProcessFlowFieldName(businessProcessFlowField.Name));

                        if (bpfField != null)
                        {
                            bpfField.Click();
                            for (int i = 0; i < businessProcessFlowField.Value.Length; i++)
                            {
                                bpfField.SendKeys(businessProcessFlowField.Value.Substring(i, 1));
                            }
                        }
                    }

                    //Click the Next Stage Button
                    var nextButton = control.FindElement(BusinessProcessFlowElementsLocators.NextStageButton);
                    nextButton.Click();
                }

                return true;
            });
        }

        internal BrowserCommandResult<bool> SelectStage(string stageName, int thinkTime = Constants.DefaultThinkTime)
        {
            return Client.Execute(Client.GetOptions($"Select Stage: {stageName}"), driver =>
            {
                //Find the Business Process Stages
                var processStages = driver.FindElements(BusinessProcessFlowElementsLocators.NextStage_UCI);

                foreach (var processStage in processStages)
                {
                    var divs = processStage.FindElements(By.TagName("div"));

                    //Click the Label of the Process Stage if found
                    foreach (var div in divs)
                    {
                        if (div.Text.Equals(stageName, StringComparison.OrdinalIgnoreCase))
                        {
                            div.Click();
                        }
                    }
                }

                driver.WaitForTransaction();

                return true;
            });
        }

        internal BrowserCommandResult<bool> SetActive(string stageName = "", int thinkTime = Constants.DefaultThinkTime)
        {
            Client.ThinkTime(thinkTime);

            return Client.Execute(Client.GetOptions($"Set Active Stage: {stageName}"), driver =>
            {
                if (!String.IsNullOrEmpty(stageName))
                {
                    SelectStage(stageName);

                    if (!driver.HasElement(By.XPath("//button[contains(@data-id,'setActiveButton')]")))
                        throw new NotFoundException($"Unable to find the Set Active button. Please verify the stage name {stageName} is correct.");

                    driver.FindElement(BusinessProcessFlowElementsLocators.SetActiveButton).Click(true);

                    driver.WaitForTransaction();
                }

                return true;
            });
        }
    }
}