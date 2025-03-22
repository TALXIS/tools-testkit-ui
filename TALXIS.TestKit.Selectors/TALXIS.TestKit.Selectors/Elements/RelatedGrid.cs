// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using OpenQA.Selenium;
using TALXIS.TestKit.Selectors.WebClientManagement;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors
{
    /// <summary>
    ///  Related Grid class.
    ///  </summary>
    public class RelatedGrid : Element
    {
        private readonly GridManager _manager;
        public RelatedGrid(WebClient client) : base()
        {
            _manager = new GridManager(client);
        }

        /// <summary>
        /// Opens a record from a related grid
        /// </summary>
        /// <param name="index">Index of the record to open</param>
        public void OpenGridRow(int index)
        {
            _manager.OpenRelatedGridRow(index);
        }

        /// <summary>
        /// Clicks a button in the Related grid menu
        /// </summary>
        /// <param name="name">Name of the button to click</param>
        /// <param name="subName">Name of the submenu button to click</param>
        public void ClickCommand(string name, string subName = null, string subSecondName = null)
        {
            _manager.ClickRelatedCommand(name, subName, subSecondName);
        }

        public IWebElement GetButtonInFlyout(string button)
        {
            return _manager.GetButtonInFlyout(button);
        }
    }
}
