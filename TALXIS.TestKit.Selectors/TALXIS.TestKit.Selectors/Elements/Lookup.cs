// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System.Collections.Generic;
using TALXIS.TestKit.Selectors.WebClientManagement;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors
{
    public class Lookup : Element
    {
        private readonly LookupManager _manager;

        public Lookup(WebClient client) : base()
        {
            _manager = new LookupManager(client);
        }

        /// <summary>
        /// Opens a record from a lookup control
        /// </summary>
        /// <param name="index">Index of the row to open</param>
        public void OpenRecord(int index)
        {
            _manager.OpenLookupRecord(index);
        }

        public void SelectLookupResult(string searchTerm)
        {
            _manager.SelectLookupResult(searchTerm);
        }

        public void SearchInLookup(string searchCriteria, LookupItem control)
        {
            _manager.SearchInLookup( searchCriteria,  control);
        }

        public IEnumerable<string> GetLookupRecordNames()
        {
            return _manager.GetLookupRecordNames();
        }

        public void SelectRelatedLookupRecord(string lookupName)
        {
            _manager.SelectRelatedLookupRecord(lookupName);
        }

        public void OpenAdvancedLookupDialog(string lookupField)
        {
            _manager.OpenAdvancedLookupDialog(lookupField);
        }

        public void ClickAddButton()
        {
            _manager.ClickAddButton();
        }

        /// <summary>
        /// Clicks the New button in a lookup control
        /// </summary>
        public void New()
        {
            _manager.SelectLookupNewButton();
        }

        /// <summary>
        /// Searches records in a lookup control
        /// </summary>
        /// <param name="control">LookupItem with the name of the lookup field</param>
        /// <param name="searchCriteria">Criteria used for search</param>
        public void Search(LookupItem control, string searchCriteria)
        {
            _manager.SearchLookupField(control, searchCriteria);
        }

        /// <summary>
        /// Selects a related entity on a lookup control
        /// </summary>
        /// <param name="entityLabel">Name of the entity to select</param>
        public void SelectRelatedEntity(string entityLabel)
        {
            _manager.SelectLookupRelatedEntity(entityLabel);
        }

        /// <summary>
        /// Clicks the "Change View" button on a lookup control and selects the view provided
        /// </summary>
        /// <param name="viewName">Name of the view to select</param>
        public void SwitchView(string viewName)
        {
            _manager.SwitchLookupView(viewName);
        }



    }
}
