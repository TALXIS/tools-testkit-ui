// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using TALXIS.TestKit.Selectors.WebClientManagement;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors
{
    public class GlobalSearch : Element
    {
        private readonly GlobalSearchManager _manager;

        public GlobalSearch(WebClient client) : base()
        {
            _manager = new GlobalSearchManager(client);
        }

        /// <summary>
        /// Search using Relevance Search or Categorized Search.
        /// </summary>
        /// <param name="criteria">Criteria to search for.</param>
        /// <returns></returns>
        public bool Search(string criteria)
        {
            return _manager.GlobalSearch(criteria);
        }

        /// <summary>
        /// Filter by entity in the Global Search Results.
        /// </summary>
        /// <param name="entity">The entity you want to filter with.</param>
        /// <example>xrmBrowser.GlobalSearch.FilterWith("Account");</example>
        public bool FilterWith(string entity)
        {
            return _manager.FilterWith(entity);
        }

        /// <summary>
        /// Filter by value in the Global Search Results.
        /// </summary>
        /// <param name="filterBy">The Group you want to filter on.</param>
        /// <param name="value">The value you want to filter on.</param>
        /// <example>xrmBrowser.GlobalSearch.Filter("Record Type", "Accounts");</example>
        public bool Filter(string filterBy, string value)
        {
            return _manager.Filter(filterBy, value);
        }

        /// <summary>
        /// Opens the specified record in the Global Search Results.
        /// </summary>
        /// <param name="entity">The entity you want to open a record.</param>
        /// <param name="index">The index of the record you want to open.</param>
        /// <example>xrmBrowser.GlobalSearch.OpenRecord("Accounts",0);</example>
        public bool OpenRecord(string entity, int index)
        {
            return _manager.OpenGlobalSearchRecord(entity, index);
        }
    }
}
