using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class GridElementsLocators
    {
        internal static By Container => By.XPath(AppElements.Xpath[AppReference.Grid.Container]);
        internal static By PcfContainer => By.XPath(AppElements.Xpath[AppReference.Grid.PcfContainer]);
        internal static By QuickFind => By.XPath(AppElements.Xpath[AppReference.Grid.QuickFind]);
        internal static By NextPage => By.XPath(AppElements.Xpath[AppReference.Grid.NextPage]);
        internal static By HideChart => By.XPath(AppElements.Xpath[AppReference.Grid.HideChart]);
        internal static By PreviousPage => By.XPath(AppElements.Xpath[AppReference.Grid.PreviousPage]);
        internal static By FirstPage => By.XPath(AppElements.Xpath[AppReference.Grid.FirstPage]);
        internal static By SelectAll => By.XPath(AppElements.Xpath[AppReference.Grid.SelectAll]);
        internal static By ShowChart => By.XPath(AppElements.Xpath[AppReference.Grid.ShowChart]);
        internal static By JumpBar => By.XPath(AppElements.Xpath[AppReference.Grid.JumpBar]);
        internal static By FilterByAll => By.XPath(AppElements.Xpath[AppReference.Grid.FilterByAll]);
        internal static By RowsContainerCheckbox => By.XPath(AppElements.Xpath[AppReference.Grid.RowsContainerCheckbox]);
        internal static By RowsContainer => By.XPath(AppElements.Xpath[AppReference.Grid.RowsContainer]);
        internal static By LegacyReadOnlyRows => By.XPath(AppElements.Xpath[AppReference.Grid.LegacyReadOnlyRows]);
        internal static By Rows => By.XPath(AppElements.Xpath[AppReference.Grid.Rows]);
        internal static By Row(string index) => By.XPath(AppElements.Xpath[AppReference.Grid.Row].Replace("[INDEX]", index));
        internal static By LastRow => By.XPath(AppElements.Xpath[AppReference.Grid.LastRow]);
        internal static By Control => By.XPath(AppElements.Xpath[AppReference.Grid.Control]);
        internal static By Columns => By.XPath(AppElements.Xpath[AppReference.Grid.Columns]);
        internal static By ChartSelector => By.XPath(AppElements.Xpath[AppReference.Grid.ChartSelector]);
        internal static By ChartViewList => By.XPath(AppElements.Xpath[AppReference.Grid.ChartViewList]);
        internal static By SortColumn(string colName) => By.XPath(AppElements.Xpath[AppReference.Grid.GridSortColumn].Replace("[COLNAME]", colName));
        internal static By Cells => By.XPath(AppElements.Xpath[AppReference.Grid.Cells]);
        internal static By CellContainer => By.XPath(AppElements.Xpath[AppReference.Grid.CellContainer]);
        internal static By ViewSelector => By.XPath(AppElements.Xpath[AppReference.Grid.ViewSelector]);
        internal static By ViewContainer => By.XPath(AppElements.Xpath[AppReference.Grid.ViewContainer]);
        internal static By ViewSelectorMenuItem => By.XPath(AppElements.Xpath[AppReference.Grid.ViewSelectorMenuItem]);
        internal static By SubArea(string name) => By.XPath(AppElements.Xpath[AppReference.Grid.SubArea].Replace("[NAME]", name));
    }
}
