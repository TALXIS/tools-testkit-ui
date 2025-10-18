namespace TALXIS.TestKit.Bindings.Steps
{
    using FluentAssertions;
    using OpenQA.Selenium;
    using Reqnroll;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using TALXIS.TestKit.Bindings.Extensions;
    using TALXIS.TestKit.Selectors;
    using TALXIS.TestKit.Selectors.Browser;
    using TALXIS.TestKit.Selectors.DTO;

    /// <summary>
    /// Step bindings related to dialogs.
    /// </summary>
    [Binding]
    public class DialogSteps : PowerAppsStepDefiner
    {
        /// <summary>
        /// Clicks the confirmation button on a confirm dialog.
        /// </summary>
        /// <param name="option">The option to click.</param>
        [When(@"I (confirm|cancel) when presented with the confirmation dialog")]
        public static void WhenIConfirmWhenPresentedWithTheConfirmationDialog(string option)
        {
            XrmApp.Dialogs.ConfirmationDialog(option == "confirm");
            XrmApp.ThinkTime(2000);
        }

        /// <summary>
        /// Assigns to the current user.
        /// </summary>
        [When("I assign to me on the assign dialog")]
        public static void WhenIAssignToMeOnTheAssignDialog()
        {
            XrmApp.Dialogs.Assign(Dialogs.AssignTo.Me);
        }

        /// <summary>
        /// Assigns to a user or team with the given name.
        /// </summary>
        /// <param name="assignTo">User or team.</param>
        /// <param name="userName">The name of the user or team.</param>
        [When("I assign to a (user|team) named '(.*)' on the assign dialog")]
        public static void WhenIAssignToANamedOnTheAssignDialog(Dialogs.AssignTo assignTo, string userName)
        {
            XrmApp.Dialogs.Assign(assignTo, userName);
        }

        /// <summary>
        /// Closes an opportunity.
        /// </summary>
        /// <param name="status">Whether the opportunity was won.</param>
        [When("I close the opportunity as (won|lost)")]
        public static void WhenICloseTheOpportunityAs(string status)
        {
            XrmApp.Dialogs.CloseOpportunity(status == "won");
        }

        /// <summary>
        /// Closes a alert dialog.
        /// </summary>
        [When("I close the alert dialog")]
        public static void WhenICloseTheAlertDialog()
        {
            XrmApp.Dialogs.CloseWarningDialog();
        }

        /// <summary>
        /// Clicks an option on the publish dialog.
        /// </summary>
        /// <param name="option">The option to click.</param>
        [When("I click (confirm|cancel) on the publish dialog")]
        public static void WhenIClickOnThePublishDialog(string option)
        {
            XrmApp.Dialogs.PublishDialog(option == "confirm");
        }

        /// <summary>
        /// Clicks an option on the set state dialog.
        /// </summary>
        /// <param name="option">The option to click.</param>
        [When("I click (ok|cancel) on the set state dialog")]
        public static void WhenIClickOnTheSetStateDialog(string option)
        {
            XrmApp.Dialogs.SetStateDialog(option == "ok");
        }

        /// <summary>
        /// Check if an alert dialog is displayed.
        /// </summary>
        [Then(@"an alert dialog should be displayed")]
        public static void ThenAlertDialogIsDisplayed()
        {
            var dialog = XrmApp.Dialogs.GetAlertDialog();

            dialog.Should().NotBeNull();
        }

        /// <summary>
        /// Check if an alert dialog with specified title is displayed.
        /// </summary>
        /// <param name="expectedTitle">The title of the alert dialog that is expected.</param>
        [Then(@"an alert dialog with title '(.*)' should be displayed")]
        public static void ThenAlertDialogWithTextIsDisplayed(string expectedTitle)
        {
            var dialogText = XrmApp.Dialogs.GetAlertDialogMessageText();

            dialogText.Should().Be(expectedTitle);
        }

        /// <summary>
        /// Check if an alert dialog with specified text is displayed.
        /// </summary>
        /// <param name="expectedText">The text of the alert dialog that is expected.</param>
        [Then(@"an alert dialog should be displayed with the text '(.*)'")]
        public static void ThenAnAlertDialogShouldBeDisplayedWithTheText(string expectedText)
        {
            XrmApp.Dialogs.CompareAllertDialog(expectedText).Should().BeTrue();
        }

        /// <summary>
        /// Sets the value for the field.
        /// </summary>
        /// <param name="fieldValue">The field value.</param>
        /// <param name="fieldLabel">The field name.</param>
        [When(@"I enter '(.*)' into the '(.*)' field in the dialog form")]
        public static void WhenIEnterInTheField(string fieldValue, string fieldLabel)
        {
            Driver.WaitForPageToLoad();

            string fieldLogicalName = XrmApp.Entity.GetFieldLogicalNameFromLabel(Driver, fieldLabel);

            string fieldType = MetadataHelper.GetFieldTypeFromDomByLogicalName(fieldLogicalName);
            string fieldLocation = MetadataHelper.GetFieldLocationFromDomByLogicalName(fieldLogicalName);
            File.AppendAllText("thx.txt", $"fieldLogicalName:{fieldLogicalName} || fieldType:{fieldType} || fieldLocation:{fieldLocation} || fieldValue.ReplaceTemplatedText():{fieldValue.ReplaceTemplatedText()}");


            if (fieldLocation == "field")
            {
                SetValueToFieldInDialogForm(fieldLogicalName, fieldValue.ReplaceTemplatedText(), fieldType);
            }
            else
            {
                throw new Exception("Unknone field location");
            }

            Client.TryLoseFocus();

            Driver.WaitForTransaction();
        }



        /// <summary>
        /// Sets the values of the fields in the table on the form.
        /// </summary>
        /// <param name="fields">The fields to set.</param>
        [When(@"I enter the following into the dialog form")]
        public static void WhenIEnterTheFollowingIntoTheForm(Table fields)
        {
            fields = fields ?? throw new ArgumentNullException(nameof(fields));

            foreach (DataTableRow row in fields.Rows)
            {
                WhenIEnterInTheField(row["Value"], row["Field"]);
            }
        }

        [When(@"I click on '(.*)' button in dialog")]
        public static void ClickButtonInDialogWindow(string buttonLabel)
        {
            XrmApp.Dialogs.SelectButtonInDialogWindow(buttonLabel);

            if (buttonLabel.ToLower().Contains("save"))
            {
                XrmApp.CommandBar.CallOnSave();
            }
        }

        private static void SetValueToFieldInDialogForm(string fieldName, string fieldValue, string fieldType)
        {

            File.AppendAllText("thx.txt", $"SetFieldValue" + Environment.NewLine);

            switch (fieldType)
            {
                case "multioptionset":
                    XrmApp.Entity.SetValue(
                        new MultiValueOptionSet()
                        {
                            Name = fieldName,
                            Values = fieldValue
                                        .Split(',')
                                        .Select(v => v.Trim())
                                        .ToArray(),
                        },
                        true);
                    break;
                case "optionset":
                    bool isNewLookEnabled = AppLookExtensions.IsNewLookEnabled(Driver);

                    XrmApp.Entity.SetValue(new OptionSet()
                    {
                        Name = fieldName,
                        Value = fieldValue,
                    },
                    FormContextType.Dialog);
                    break;
                case "boolean":
                    XrmApp.Entity.SetValue(new BooleanItem()
                    {
                        Name = fieldName,
                        Value = bool.Parse(fieldValue),
                    });
                    break;
                case "datetime":
                    XrmApp.Entity.SetValue(new DateTimeControl(fieldName)
                    {
                        // !TO-DO: Datetime formattings
                        Value = DateTime.Parse(fieldValue, CultureInfo.CurrentCulture),
                    });
                    break;
                case "lookup":
                    XrmApp.Entity.SetValue(new LookupItem()
                    {
                        Name = fieldName,
                        Value = fieldValue,
                    },
                    FormContextType.Dialog);
                    break;
                case "currency":
                case "numeric":
                case "text":
                default:
                    {
                        //XrmApp.Dialogs.SetValueToDialogForm(fieldName, fieldValue);
                        XrmApp.Entity.SetValue(fieldName, fieldValue, FormContextType.Dialog);
                    }
                    break;
            }
        }
    }
}
