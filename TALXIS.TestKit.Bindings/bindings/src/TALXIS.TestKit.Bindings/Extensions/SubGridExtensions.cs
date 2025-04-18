﻿namespace TALXIS.TestKit.Bindings.Extensions
{
    using System;
    using System.Globalization;
    using OpenQA.Selenium;
    using TALXIS.TestKit.Selectors;
    using TALXIS.TestKit.Selectors.Browser;

    /// <summary>
    /// Extensions to the <see cref="SubGrid"/> class.
    /// </summary>
    public static class SubGridExtensions
    {
        /// <summary>
        /// Gets the index of a record by ID.
        /// Accessing grid item IDs or URLs via EasyRepro is currently broken (see https://github.com/microsoft/EasyRepro/issues/800). Use this method instead.
        /// </summary>
        /// <param name="subGrid">The SubGrid.</param>
        /// <param name="subgridName">The name of the subgrid.</param>
        /// <param name="driver">The Selenium WebDriver.</param>
        /// <param name="recordId">The ID of the record.</param>
        /// <returns>The index of the record.</returns>
        public static int GetRecordIndexById(this SubGrid subGrid, string subgridName, IWebDriver driver, Guid recordId)
        {
            if (driver is null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            driver.WaitUntilAvailable(By.XPath(AppElements.Xpath[AppReference.Entity.SubGridContents].Replace("[NAME]", subgridName)));

            var index = (long)driver.ExecuteScript(
                $"return Xrm.Page.getControl(\"{subgridName}\").getGrid().getRows().get().findIndex(row => row.getData().getEntity().getId() == \"{recordId.ToString("B").ToUpper(CultureInfo.CurrentCulture)}\")");

            return Convert.ToInt32(index);
        }

        /// <summary>
        /// Gets the index of a record by ID.
        /// Accessing grid item IDs or URLs via EasyRepro is currently broken (see https://github.com/microsoft/EasyRepro/issues/800). Use this method instead.
        /// </summary>
        /// <param name="subGrid">The SubGrid.</param>
        /// <param name="subgridName">The name of the subgrid.</param>
        /// <param name="driver">The Selenium WebDriver.</param>
        /// <param name="index">The index of the record.</param>
        public static void HighlightRecord(this SubGrid subGrid, string subgridName, IWebDriver driver, int index)
        {
            if (driver is null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            var subGridElement = driver.FindElement(
                By.XPath(AppElements.Xpath[AppReference.Entity.SubGridContents].Replace("[NAME]", subgridName)));
            var rows = subGridElement.FindElements(By.XPath(AppElements.Xpath[AppReference.Entity.SubGridRows]));

            if (rows.Count == 0)
            {
                throw new NoSuchElementException($"No records were found for subgrid {subgridName}");
            }

            if (index + 1 > rows.Count)
            {
                throw new IndexOutOfRangeException($"Subgrid {subgridName} record count: {rows.Count}. Expected: {index + 1}");
            }

            rows[index].FindElements(By.XPath(AppElements.Xpath[AppReference.Entity.SubGridCells]))[0].Click();
            driver.WaitForTransaction();
        }
    }
}