using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Reqnroll;

namespace TALXIS.TestKit.Bindings.Extensions
{
    internal class MetadataHelper : PowerAppsStepDefiner
    {
        /// <summary>
        /// Gets the field type from the DOM using a JavaScript query.
        /// </summary>
        internal static string GetFieldTypeFromDomByLable(string fieldLabel)
        {
            string fieldLogicalName = XrmApp.Entity.GetFieldLogicalNameFromLabel(Driver, fieldLabel);

            return GetFieldTypeFromDomByLogicalName(fieldLogicalName);
        }

        internal static string GetFieldTypeFromDomByLogicalName(string fieldLogicalName)
        {
            try
            {
                string script = @"
                    function getFieldType(fieldLogicalName) {
    
                            var attribute = Xrm.Page.getAttribute(fieldLogicalName);
                            if (!attribute) return 'attribute not found';
    
                            var type = attribute.getAttributeType();
                            switch (type) {
                                case 'boolean': return 'boolean';
                                case 'datetime': return 'datetime';
                                case 'lookup': return 'lookup';
                                case 'optionset': return 'optionset';
                                case 'multiselectoptionset': return 'multioptionset';
                                default: return 'text';
                            }
                    }

                    return getFieldType(arguments[0]);
                    ";


                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;
                object result = jsExecutor.ExecuteScript(script, fieldLogicalName);

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving field type '{fieldLogicalName}': {ex.Message}");
            }
        }

        internal static string GetFieldLocationFromDomByLogicalName(string fieldLogicalName)
        {
            try
            {
                string script = @"
                    function getFieldLocation(fieldLogicalName) {
                        var control = Xrm.Page.getControl(fieldLogicalName);
                        if (!control) return 'control not found';

                        var isHeader = Xrm.Page.ui.tabs.get('header_section') && 
                                       Xrm.Page.ui.tabs.get('header_section').controls.get(fieldLogicalName);

                        var location = isHeader ? 'header' : 'field';
                        return location;
                    }

                    return getFieldLocation(arguments[0]);
                ";

                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;
                object result = jsExecutor.ExecuteScript(script, fieldLogicalName);

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error when retrieving field location '{fieldLogicalName}': {ex.Message}");
            }
        }
    }
}
