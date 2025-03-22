// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System;
using TALXIS.TestKit.Selectors.WebClientManagement;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors
{
    public class Dashboard : Element
    {
        private readonly DashboardManager _manager;

        public SubGrid SubGrid => this.GetElement<SubGrid>(_manager.Client);

        public Dashboard(WebClient client)
        {
            _manager = new DashboardManager(client);
        }

        public T GetElement<T>(WebClient client)
            where T : Element
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { client });
        }

        /// <summary>
        /// Selects the Dashboard provided
        /// </summary>
        /// <param name="dashboardName">Name of the dashboard to select</param>
        public void SelectDashboard(string dashboardName)
        {
            _manager.SelectDashboard(dashboardName);
        }
    }
}
