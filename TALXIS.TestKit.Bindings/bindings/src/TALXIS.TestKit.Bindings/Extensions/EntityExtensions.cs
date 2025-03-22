﻿namespace TALXIS.TestKit.Bindings.Extensions
{
    using System;
    using OpenQA.Selenium;
    using TALXIS.TestKit.Selectors;

    /// <summary>
    /// Extensions to the <see cref="Entity"/> class.
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// This is required as <see cref="Entity.GetField"/> throws a <see cref="NullReferenceException"/> when getting a hidden field.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="driver">The Selenium WebDriver.</param>
        /// <param name="fieldName">The name of the field.</param>
        /// <returns>Whether the field is visible.</returns>
        public static bool IsFieldVisible(this Entity entity, IWebDriver driver, string fieldName)
        {
            driver = driver ?? throw new ArgumentNullException(nameof(driver));

            var elements = driver.FindElements(By.CssSelector($"div[data-id={fieldName}]"));

            return elements.Count > 0;
        }

        /// <summary>
        /// Gets logical name of a field on form from its label.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="driver">The Selenium WebDriver.</param>
        /// <param name="fieldLabel">The label of the field.</param>
        /// <returns>Logical name of the field.</returns>
        public static string GetFieldLogicalNameFromLabel(this Entity entity, IWebDriver driver, string fieldLabel)
        {
            driver = driver ?? throw new ArgumentNullException(nameof(driver));

            var labelElement = driver.FindElement(By.XPath($"//label[text()='{fieldLabel}']/ancestor-or-self::div[@data-preview_orientation='row'][1]"));
            string logicalName = labelElement.GetAttribute("data-id");

            return logicalName;
        }

        /// <summary>
        /// Determines if the section on the form is visible.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="driver">The Selenium WebDriver.</param>
        /// <param name="sectionName">The name of the section.</param>
        /// <returns>Whether the section is visible.</returns>
        public static bool IsSectionVisible(this Entity entity, IWebDriver driver, string sectionName)
        {
            driver = driver ?? throw new ArgumentNullException(nameof(driver));

            var elements = driver.FindElements(By.CssSelector($"*[data-id={sectionName}]"));

            return elements.Count > 0;
        }
    }
}
