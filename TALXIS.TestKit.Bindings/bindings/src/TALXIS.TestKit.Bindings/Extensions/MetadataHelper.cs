using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using OpenQA.Selenium;
using Reqnroll;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace TALXIS.TestKit.Bindings.Extensions
{
    internal class MetadataHelper : PowerAppsStepDefiner
    {

        public static IWebElement FindFieldByLogicalName( string logicalName)
        {
            if (string.IsNullOrWhiteSpace(logicalName))
                throw new ArgumentNullException(nameof(logicalName));

            IWebElement root = null;
            try
            {
                root = Driver.FindElement(By.CssSelector("div[role='dialog']"));
            }
            catch (NoSuchElementException)
            {
                root = Driver.FindElement(By.TagName("body"));
            }

            var selector = $"[data-id='{logicalName}.fieldControl'], [data-id='{logicalName}']";
            var element = root.FindElement(By.CssSelector(selector));

            return element;
        }

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

        internal static Dictionary<string, string> GetAttributeLabels(ServiceClient serviceClient, string entityLogicalName)
        {
            var request = new RetrieveEntityRequest
            {
                LogicalName = entityLogicalName,
                EntityFilters = EntityFilters.Attributes,
                RetrieveAsIfPublished = true
            };

            var response = (RetrieveEntityResponse)serviceClient.Execute(request);
            var metadata = response.EntityMetadata;

            var result = new Dictionary<string, string>();

            foreach (var attr in metadata.Attributes)
            {
                var label = attr.DisplayName?.UserLocalizedLabel?.Label
                            ?? attr.DisplayName?.LocalizedLabels?.FirstOrDefault()?.Label;

                if (!string.IsNullOrEmpty(label) && !string.IsNullOrEmpty(attr.LogicalName))
                {
                    result[label] = attr.LogicalName;
                }
            }

            return result;
        }

        internal static string GetFirstSubgridName()
        {
            string script = @"
                    function getFirstSubgridName() {
    
                        Xrm.Page.ui.controls.forEach(function(c) {
                            if (c.getControlType() === ""subgrid"") console.log(c.getName());
                        });
                    }

                    return getFirstSubgridName(arguments[0]);
            ";


            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;
            object result = jsExecutor.ExecuteScript(script);

            return result.ToString();
        }

    }
}
