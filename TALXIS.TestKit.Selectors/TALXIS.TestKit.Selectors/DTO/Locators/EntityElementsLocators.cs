﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenQA.Selenium;

namespace TALXIS.TestKit.Selectors.DTO.Locators
{
    internal class EntityElementsLocators
    {
        internal static By FormContext => By.XPath(AppElements.Xpath[AppReference.Entity.FormContext]);
        internal static By FormSelector => By.XPath(AppElements.Xpath[AppReference.Entity.FormSelector]);
        internal static By HeaderTitle => By.XPath(AppElements.Xpath[AppReference.Entity.HeaderTitle]);
        internal static By HeaderContext => By.XPath(AppElements.Xpath[AppReference.Entity.HeaderContext]);
        internal static By Save => By.XPath(AppElements.Xpath[AppReference.Entity.Save]);
        internal static By TextFieldContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.TextFieldContainer].Replace("[NAME]", name));
        internal static By TextFieldLabel(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.TextFieldLabel].Replace("[NAME]", name));
        internal static By TextFieldValue(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.TextFieldValue].Replace("[NAME]", name));
        internal static By TextFieldLookup => By.XPath(AppElements.Xpath[AppReference.Entity.TextFieldLookup]);
        internal static By TextFieldLookupSearchButton(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.TextFieldLookupSearchButton].Replace("[NAME]", name));
        internal static By TextFieldLookupMenu(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.TextFieldLookupMenu].Replace("[NAME]", name));
        internal static By LookupFieldExistingValue(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.LookupFieldExistingValue].Replace("[NAME]", name));
        internal static By LookupFieldDeleteExistingValue(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.LookupFieldDeleteExistingValue].Replace("[NAME]", name));
        internal static By LookupFieldExpandCollapseButton(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.LookupFieldExpandCollapseButton].Replace("[NAME]", name));
        internal static By LookupFieldNoRecordsText(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.LookupFieldNoRecordsText].Replace("[NAME]", name));
        internal static By LookupFieldResultList(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.LookupFieldResultList].Replace("[NAME]", name));
        internal static By LookupFieldResultListItem(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.LookupFieldResultListItem].Replace("[NAME]", name));
        internal static By LookupFieldHoverExistingValue(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.LookupFieldHoverExistingValue].Replace("[NAME]", name));
        internal static By LookupResultsDropdown(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.LookupResultsDropdown].Replace("[NAME]", name));
        internal static By OptionSetFieldContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.OptionSetFieldContainer].Replace("[NAME]", name));
        internal static By TextFieldLookupFieldContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.TextFieldLookupFieldContainer].Replace("[NAME]", name));
        internal static By RecordSetNavigator => By.XPath(AppElements.Xpath[AppReference.Entity.RecordSetNavigator]);
        internal static By RecordSetNavigatorOpen => By.XPath(AppElements.Xpath[AppReference.Entity.RecordSetNavigatorOpen]);
        internal static By RecordSetNavList => By.XPath(AppElements.Xpath[AppReference.Entity.RecordSetNavList]);
        internal static By RecordSetNavCollapseIcon => By.XPath(AppElements.Xpath[AppReference.Entity.RecordSetNavCollapseIcon]);
        internal static By RecordSetNavCollapseIconParent => By.XPath(AppElements.Xpath[AppReference.Entity.RecordSetNavCollapseIconParent]);
        internal static By FieldControlDateTimeContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.FieldControlDateTimeContainer].Replace("[NAME]", name));
        internal static By FieldControlDateTimeInputUCI(string field) => By.XPath(AppElements.Xpath[AppReference.Entity.FieldControlDateTimeInputUCI].Replace("[FIELD]", field));
        internal static By FieldControlDateTimeTimeInputUCI(string field) => By.XPath(AppElements.Xpath[AppReference.Entity.FieldControlDateTimeTimeInputUCI].Replace("[FIELD]", field));
        internal static By Delete => By.XPath(AppElements.Xpath[AppReference.Entity.Delete]);
        internal static By Assign => By.XPath(AppElements.Xpath[AppReference.Entity.Assign]);
        internal static By SwitchProcess => By.XPath(AppElements.Xpath[AppReference.Entity.SwitchProcess]);
        internal static By CloseOpportunityWin => By.XPath(AppElements.Xpath[AppReference.Entity.CloseOpportunityWin]);
        internal static By CloseOpportunityLoss => By.XPath(AppElements.Xpath[AppReference.Entity.CloseOpportunityLoss]);
        internal static By ProcessButton => By.XPath(AppElements.Xpath[AppReference.Entity.ProcessButton]);
        internal static By SwitchProcessDialog => By.XPath(AppElements.Xpath[AppReference.Entity.SwitchProcessDialog]);
        internal static By TabList => By.XPath(AppElements.Xpath[AppReference.Entity.TabList]);
        internal static By Tab(string title) => By.XPath(string.Format(AppElements.Xpath[AppReference.Entity.Tab], title));
        internal static By MoreTabs => By.XPath(AppElements.Xpath[AppReference.Entity.MoreTabs]);
        internal static By MoreTabsMenu => By.XPath(AppElements.Xpath[AppReference.Entity.MoreTabsMenu]);
        internal static By SubTab => By.XPath(AppElements.Xpath[AppReference.Entity.SubTab]);
        internal static By SubGridTitle(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridTitle].Replace("[NAME]", name));
        internal static By SubGridRow(string name, string index) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridRow].Replace("[NAME]", name).Replace("[INDEX]", index));
        internal static By SubGrid_LastRow => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridLastRow]);
        internal static By SubGridContents(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridContents].Replace("[NAME]", name));
        internal static By SubGridList(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridList].Replace("[NAME]", name));
        internal static By SubGridListCells(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridListCells].Replace("[NAME]", name));
        internal static By SubGridViewPickerButton => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridViewPickerButton]);
        internal static By SubGridViewPickerFlyout => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridViewPickerFlyout]);
        internal static By SubGridCommandBar(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridCommandBar].Replace("[NAME]", name));
        internal static By SubGridCommandLabel(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridCommandLabel].Replace("[NAME]", name));
        internal static By SubGridOverflowContainer => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridOverflowContainer]);
        internal static By SubGridOverflowButton(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridOverflowButton].Replace("[NAME]", name));
        internal static By SubGridHighDensityList(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridHighDensityList].Replace("[NAME]", name));
        internal static By EditableSubGridList(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EditableSubGridList].Replace("[NAME]", name));
        internal static By EditableSubGridListCells => By.XPath(AppElements.Xpath[AppReference.Entity.EditableSubGridListCells]);
        internal static By EditableSubGridListCellRows => By.XPath(AppElements.Xpath[AppReference.Entity.EditableSubGridListCellRows]);
        internal static By EditableSubGridCells => By.XPath(AppElements.Xpath[AppReference.Entity.EditableSubGridCells]);
        internal static By SubGridControl => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridControl]);
        internal static By SubGridCells => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridCells]);
        internal static By SubGridRows => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridRows]);
        internal static By SubGridRowsHighDensity => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridRowsHighDensity]);
        internal static By SubGridDataRowsEditable => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridDataRowsEditable]);
        internal static By SubGridHeaders => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridHeaders]);
        internal static By SubGridHeadersHighDensity => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridHeadersHighDensity]);
        internal static By SubGridHeadersEditable => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridHeadersEditable]);
        internal static By SubGridRecordCheckbox(string name, string index) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridRecordCheckbox].Replace("[NAME]", name).Replace("[INDEX]", index));
        internal static By SubGridSearchBox => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridSearchBox]);
        internal static By SubGridAddButton(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.SubGridAddButton].Replace("[NAME]", name));
        internal static By FieldLookupButton(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.FieldLookupButton].Replace("[NAME]", name));
        internal static By SearchButtonIcon => By.XPath(AppElements.Xpath[AppReference.Entity.SearchButtonIcon]);
        internal static By DuplicateDetectionWindowMarker => By.XPath(AppElements.Xpath[AppReference.Entity.DuplicateDetectionWindowMarker]);
        internal static By DuplicateDetectionGridRows => By.XPath(AppElements.Xpath[AppReference.Entity.DuplicateDetectionGridRows]);
        internal static By DuplicateDetectionIgnoreAndSaveButton => By.XPath(AppElements.Xpath[AppReference.Entity.DuplicateDetectionIgnoreAndSaveButton]);
        internal static By EntityBooleanFieldRadioContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldRadioContainer].Replace("[NAME]", name));
        internal static By EntityBooleanFieldRadioTrue(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldRadioTrue].Replace("[NAME]", name));
        internal static By EntityBooleanFieldRadioFalse(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldRadioFalse].Replace("[NAME]", name));
        internal static By EntityBooleanFieldButtonContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldButtonContainer].Replace("[NAME]", name));
        internal static By EntityBooleanFieldButtonTrue => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldButtonTrue]);
        internal static By EntityBooleanFieldButtonFalse => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldButtonFalse]);
        internal static By EntityBooleanFieldCheckboxContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldCheckboxContainer].Replace("[NAME]", name));
        internal static By EntityBooleanFieldCheckbox(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldCheckbox].Replace("[NAME]", name));
        internal static By EntityBooleanFieldList(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldList].Replace("[NAME]", name));
        internal static By EntityBooleanFieldFlipSwitchLink(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldFlipSwitchLink].Replace("[NAME]", name));
        internal static By EntityBooleanFieldFlipSwitchContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldFlipSwitchContainer].Replace("[NAME]", name));
        internal static By EntityBooleanFieldToggle(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityBooleanFieldToggle].Replace("[NAME]", name));
        internal static By EntityNewLookOptionsetStatusCombo(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityNewLookOptionsetStatusCombo].Replace("[NAME]", name));
        internal static By EntityNewLookOptionsetStatusComboButton(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityNewLookOptionsetStatusComboButton].Replace("[NAME]", name));
        internal static By EntityNewLookOptionsetStatusComboList(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityNewLookOptionsetStatusComboList].Replace("[NAME]", name));
        internal static By EntityOldLookOptionsetStatusCombo(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityOldLookOptionsetStatusCombo].Replace("[NAME]", name));
        internal static By EntityOldLookOptionsetStatusComboButton(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityOldLookOptionsetStatusComboButton].Replace("[NAME]", name));
        internal static By EntityOldLookOptionsetStatusComboList(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityOldLookOptionsetStatusComboList].Replace("[NAME]", name));
        internal static By EntityOptionsetStatusTextValue(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.EntityOptionsetStatusTextValue].Replace("[NAME]", name));
        internal static By FormMessageBar => By.XPath(AppElements.Xpath[AppReference.Entity.FormMessageBar]);
        internal static By FormMessageBarTypeIcon => By.XPath(AppElements.Xpath[AppReference.Entity.FormMessageBarTypeIcon]);
        internal static By FormNotifcationBar => By.XPath(AppElements.Xpath[AppReference.Entity.FormNotifcationBar]);
        internal static By FormNotifcationExpandButton => By.XPath(AppElements.Xpath[AppReference.Entity.FormNotifcationExpandButton]);
        internal static By FormNotifcationFlyoutRoot => By.XPath(AppElements.Xpath[AppReference.Entity.FormNotifcationFlyoutRoot]);
        internal static By FormNotifcationList => By.XPath(AppElements.Xpath[AppReference.Entity.FormNotifcationList]);
        internal static By FormNotifcationTypeIcon => By.XPath(AppElements.Xpath[AppReference.Entity.FormNotifcationTypeIcon]);

        internal static class Header
        {
            internal static By Container => By.XPath(AppElements.Xpath[AppReference.Entity.Header.Container]);
            internal static By Flyout => By.XPath(AppElements.Xpath[AppReference.Entity.Header.Flyout]);
            internal static By FlyoutButton => By.XPath(AppElements.Xpath[AppReference.Entity.Header.FlyoutButton]);
            internal static By LookupFieldContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.Header.LookupFieldContainer].Replace("[NAME]", name));
            internal static By TextFieldContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.Header.TextFieldContainer].Replace("[NAME]", name));
            internal static By OptionSetFieldContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.Header.OptionSetFieldContainer].Replace("[NAME]", name));
            internal static By DateTimeFieldContainer(string name) => By.XPath(AppElements.Xpath[AppReference.Entity.Header.DateTimeFieldContainer].Replace("[NAME]", name));
        }
    }
}
