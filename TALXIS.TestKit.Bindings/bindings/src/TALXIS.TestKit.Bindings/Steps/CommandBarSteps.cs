namespace TALXIS.TestKit.Bindings.Steps
{
    using System;
    using System.IO;
    using System.Threading;
    using FluentAssertions;
    using OpenQA.Selenium;
    using Reqnroll;
    using TALXIS.TestKit.Selectors.Browser;
    using static System.Net.Mime.MediaTypeNames;

    /// <summary>
    /// Step bindings relating to the command bar.
    /// </summary>
    [Binding]
    public class CommandBarSteps : PowerAppsStepDefiner
    {
        /// <summary>
        /// Selects a command with the given label.
        /// </summary>
        /// <param name="commandName">The label of the command.</param>
        [When("I select the '(.*)' command")]
        public static void WhenISelectTheCommand(string commandName)
        {
            XrmApp.CommandBar.ClickCommand(commandName);
        }

        /// <summary>
        /// Selects a command under a flyout with the given label.
        /// </summary>
        /// <param name="commandName">The label of the command.</param>
        /// <param name="flyoutName">The label of the flyout.</param>
        [When("I select the '([^']+)' command under the '([^']+)' flyout")]
        public static void WhenISelectTheCommandUnderTheFlyout(string commandName, string flyoutName)
        {
            XrmApp.CommandBar.ClickCommand(flyoutName, commandName);
        }

        /// <summary>
        /// Asserts that a command is available in the command bar.
        /// </summary>
        /// <param name="commandName">The label of the command.</param>
        [Then("I should be able to see the '(.*)' command")]
        public static void ThenIShouldBeAbleToSeeTheCommand(string commandName)
        {
            XrmApp.CommandBar.GetCommandValues(true).Value.Should().Contain(commandName);
        }

        /// <summary>
        /// Asserts that a command is available in the command bar.
        /// </summary>
        /// <param name="commandName">The label of the command.</param>
        [Then("I should not be able to see the '(.*)' command")]
        public static void ThenIShouldNotBeAbleToSeeTheCommand(string commandName)
        {
            XrmApp.CommandBar.GetCommandValues(true).Value.Should().NotContain(commandName);
        }

        /// <summary>
        /// Selects a Plus icon button and entity with the given label under a flyout.
        /// </summary>
        /// <param name="entityName">The label of the entity.</param>
        [When("I click the Plus icon button in the Global Commands ribbon and select '(.*)' on the flyout")]
        public static void WhenIClickThePlusIconInTheGlobalCommandsRibbonAndSelectOnTheFlyout(string entityName)
        {
            XrmApp.Navigation.QuickCreate(entityName);
        }
    }
}
