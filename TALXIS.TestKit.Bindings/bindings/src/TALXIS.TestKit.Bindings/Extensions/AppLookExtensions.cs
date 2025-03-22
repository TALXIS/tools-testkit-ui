namespace TALXIS.TestKit.Bindings.Extensions
{
    using System;
    using OpenQA.Selenium;

    public static class AppLookExtensions
    {
        /// <summary>
        /// Determines if the new look is enabled for the app.
        /// </summary>
        /// <param name="driver">The Selenium WebDriver.</param>
        /// <returns>Whether the new look is enabled for the app.</returns>
        public static bool IsNewLookEnabled(IWebDriver driver)
        {
            ArgumentNullException.ThrowIfNull(driver, nameof(driver));

            try
            {
                IWebElement newLookToggleButton = driver.FindElement(By.CssSelector("button.ms-Toggle-background"));

                string isChecked = newLookToggleButton.GetAttribute("aria-checked");

                return isChecked == "true";
            }
            catch (NoSuchElementException)
            {
                return true;
            }
        }
    }
}
