// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System;
using TALXIS.TestKit.Selectors.DTO;
using TALXIS.TestKit.Selectors.WebClientManagement;
using TALXIS.TestKit.Selectors.WebClientManagement.Helpers;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors
{
    public class BusinessProcessFlow : Element
    {
        private readonly BusinessProcessFlowManager _bpfManager;
        private readonly EntityManager _entityManager;

        public BusinessProcessFlow(WebClient client)
        {
            _bpfManager = new BusinessProcessFlowManager(client);
            _entityManager = new EntityManager(client);
        }

        public Field GetField(string field)
        {
            return _bpfManager.BPFGetField(field);
        }

        /// <summary>
        /// Retrieves the value of a text field
        /// </summary>
        /// <param name="field">Schema name of the field to retrieve</param>
        /// <returns></returns>
        public string GetValue(string field)
        {
            return _entityManager.GetValue(field);
        }

        /// <summary>
        /// Retrieves the value of a Lookup field
        /// </summary>
        /// <param name="field">LookupItem with the schema name of the field to retrieve</param>
        public string GetValue(LookupItem field)
        {
            return _entityManager.GetValue(field);
        }

        /// <summary>
        /// Retrieves the value of a OptionSet field
        /// </summary>
        /// <param name="field">OptionSet with the schema name of the field to retrieve</param
        public string GetValue(OptionSet field)
        {
            return _entityManager.GetValue(field);
        }

        /// <summary>
        /// Retrieves the value of a BooleanItem field.
        /// </summary>
        /// <param name="field">BooleanItem with the schema name of the field to retrieve.</param>
        public bool GetValue(BooleanItem field)
        {
            return _entityManager.GetValue(field);
        }

        /// <summary>
        /// Sets the stage provided to Active
        /// </summary>
        /// <param name="stageName">Name of the business process flow stage</param>
        public void SetActive(string stageName = "")
        {
            _bpfManager.SetActive(stageName);
        }

        /// <summary>
        /// Clicks "Next Stage" on the stage provided
        /// </summary>
        /// <param name="stageName">Name of the business process flow stage</param>
        /// <param name="businessProcessFlowField">Optional - field to set the value on for this business process flow stage</param>
        public void NextStage(string stageName, Field businessProcessFlowField = null)
        {
            _bpfManager.NextStage(stageName, businessProcessFlowField);
        }

        /// <summary>
        /// Selects the stage provided
        /// </summary>
        /// <param name="stageName">Name of the business process flow stage</param>
        public void SelectStage(string stageName)
        {
            _bpfManager.SelectStage(stageName);
        }

        /// <summary>
        /// Sets the value of a text field
        /// </summary>
        /// <param name="field">Schema name of the field to retrieve</param>
        public void SetValue(string field, string value)
        {
            _bpfManager.BPFSetValue(field, value);
        }

        /// <summary>
        /// Sets the value of an OptionSet field
        /// </summary>
        /// <param name="field">OptionSet with the schema name of the field to retrieve</param>
        public void SetValue(OptionSet optionSet)
        {
            _bpfManager.BPFSetValue(optionSet);
        }

        /// <summary>
        /// Sets the value of a BooleanItem field
        /// </summary>
        /// <param name="field">BooleanItem with the schema name of the field to retrieve</param>
        public void SetValue(BooleanItem optionSet)
        {
            _bpfManager.BPFSetValue(optionSet);
        }

        /// <summary>
        /// Sets the value of a LookupItem field
        /// </summary>
        /// <param name="field">LookupItem with the schema name of the field to retrieve</param>
        public void SetValue(LookupItem control)
        {
            SetValueHelper.SetLookUp(_bpfManager.Client,control, FormContextType.BusinessProcessFlow);
        }

        /// <summary>
        /// Sets the value of a Date field
        /// </summary>
        /// <param name="field">Schema name of the field to retrieve</param>
        public void SetValue(string field, DateTime date, string formatDate = null, string formatTime = null)
        {
            SetValueHelper.SetDateTimeValue(_bpfManager.Client, field, date, FormContextType.BusinessProcessFlow, formatDate, formatTime);
        }

        /// <summary>
        /// Sets the value of a MultiValueOptionSet field
        /// </summary>
        /// <param name="field">MultiValueOptionSet with the schema name of the field to retrieve</param>
        public void SetValue(MultiValueOptionSet option, bool removeExistingValues = false)
        {
            SetValueHelper.SetMultiSelectOptionSetValue(_bpfManager.Client, option, FormContextType.BusinessProcessFlow, removeExistingValues);
        }

        /// <summary>
        /// Pins the Business Process Flow Stage to the right side of the window
        /// </summary>
        /// <param name="stageName">The name of the Business Process Flow Stage</param>
        public void Pin(string stageName)
        {
            _bpfManager.BPFPin(stageName);
        }

        /// <summary>
        /// Clicks the "X" button in the Business Process Flow flyout menu for the Stage provided
        /// </summary>
        /// <param name="stageName">Name of the business process flow stage</param>
        public void Close(string stageName)
        {
            _bpfManager.BPFClose(stageName);
        }
    }
}
