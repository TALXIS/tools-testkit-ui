﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;
using OpenQA.Selenium;
using TALXIS.TestKit.Selectors.WebClientManagement;
using TALXIS.TestKit.Selectors.WebClientManagement.Helpers;
using TALXIS.TestKit.Selectors.Browser;
using TALXIS.TestKit.Selectors.Browser.Extensions;

namespace TALXIS.TestKit.Selectors
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="TALXIS.TestKit.Selectors.Browser.Element" />
    public class Telemetry : Element
    {
        private readonly PerformanceCenterManger _manger;

        public Telemetry(WebClient client)
        {
            _manger = new PerformanceCenterManger(client);
        }

        /// <summary>
        /// Enables Performance Center for UCI 
        /// </summary>
        public void EnablePerformanceCenter()
        {
            _manger.EnablePerformanceCenter();
        }

        public enum BrowserEventType
        {
            Resource,
            Navigation,
            Server
        }

        /// <summary>
        /// Track Command Events will track the Command Results from the API executions to Application Insights with the Instrumentation Key provided in the Options. 
        /// </summary>
        /// <param name="additionalProperties">The additional properties you want to track in telemetry. These values will show up in the customDimensions of the customEvents</param>
        /// <param name="additionalMetrics">The additional metricsyou want to track in telemetry. These values will show up in the customMeasurements of the customEvents</param>
        public void TrackCommandEvents(Dictionary<string, string> additionalProperties = null, Dictionary<string, double> additionalMetrics = null)
        {
            _manger.Client.CommandResults.ForEach(x =>
            {
                var properties = new Dictionary<string, string>();
                var metrics = new Dictionary<string, double>();

                if (additionalMetrics != null && additionalMetrics.Count > 0)
                    metrics = metrics.Merge(additionalMetrics);

                if (additionalProperties != null && additionalProperties.Count > 0)
                    properties = properties.Merge(additionalProperties);

                properties.Add("StartTime", x.StartTime.Value.ToLongDateString());
                properties.Add("EndTime", x.StopTime.Value.ToLongDateString());

                metrics.Add("ThinkTime", x.ThinkTime);
                metrics.Add("TransitionTime", x.TransitionTime);
                metrics.Add("ExecutionTime", x.ExecutionTime);
                metrics.Add("ExecutionAttempts", x.ExecutionAttempts);

                TrackEvents(x.CommandName, properties, metrics);
            });
        }
        /// <summary>
        /// Tracks the performance center telemetry events.
        /// </summary>
        /// <param name="additionalProperties">The additional properties you want to track in telemetry. These values will show up in the customDimensions of the customEvents</param>
        /// <param name="additionalMetrics">The additional metricsyou want to track in telemetry. These values will show up in the customMeasurements of the customEvents</param>
        /// <exception cref="System.InvalidOperationException">UCI Performance Mode is not enabled.  Please enable performance mode in the Options before tracking performance telemetry.</exception>
        public void TrackPerformanceEvents(Dictionary<string, string> additionalProperties = null, Dictionary<string, double> additionalMetrics = null)
        {
            if (!_manger.Client.Browser.Options.UCIPerformanceMode) throw new InvalidOperationException("UCI Performance Mode is not enabled.  Please enable performance mode in the Options before tracking performance telemetry.");
            ShowHidePerformanceWidget();

            Dictionary<string, string> metadata = GetMetadataMarkers();
            Dictionary<string, double> markers = GetPerformanceMarkers(GetRecentPageName());

            ShowHidePerformanceWidget();

            if (additionalMetrics != null && additionalMetrics.Count > 0)
                markers = markers.Merge(additionalMetrics);

            if (additionalProperties != null && additionalProperties.Count > 0)
                metadata = metadata.Merge(additionalProperties);

            TrackEvents("Performance Markers", metadata, markers);


        }
        /// <summary>
        /// Send Exception to Application Insights
        /// </summary>
        /// <param name="exception">System.Exception object containing message and stack that you want to track.</param>
        /// <param name="additionalProperties"></param>
        /// <param name="additionalMetrics"></param>
        public void TrackException(Exception exception, Dictionary<string, string> additionalProperties = null, Dictionary<string, double> additionalMetrics = null)
        {

            if (string.IsNullOrEmpty(_manger.Client.Browser.Options.AppInsightsKey)) throw new InvalidOperationException("The Application Insights key was not specified.  Please specify an Instrumentation key in the Browser Options.");

            //var telemetry = new Microsoft.ApplicationInsights.TelemetryClient { InstrumentationKey = _manger.Client.Browser.Options.AppInsightsKey };

            var telemetryConfig = TelemetryConfiguration.CreateDefault();
            telemetryConfig.ConnectionString = _manger.Client.Browser.Options.AppInsightsKey;
            var telemetry = new TelemetryClient(telemetryConfig);

            ExceptionTelemetry exceptionTelemetry = new ExceptionTelemetry();
            exceptionTelemetry.Exception = exception;

            telemetry.TrackException(exception, additionalProperties, additionalMetrics);
            telemetry.Flush();

            telemetry = null;
        }

        /// <summary>
        /// Tracks the browser window.performance events.
        /// </summary>
        /// <param name="type">The type of window.performance timings you want to track.</param>
        /// <param name="additionalProperties">The additional properties you want to track in telemetry. These values will show up in the customDimensions of the customEvents</param>
        /// <param name="clearTimings">if set to <c>true</c> clears the resource timings.</param>
        public void TrackBrowserEvents(BrowserEventType type, Dictionary<string, string> additionalProperties = null, bool clearTimings = false)
        {
            var properties = GetBrowserTimings(type);

            if (additionalProperties != null) properties = properties.Merge(additionalProperties);

            if (properties.Count > 0) TrackEvents(type.ToString(), properties, null);
        }

        internal void TrackEvents(string eventName, Dictionary<string, string> properties, Dictionary<string, double> metrics)
        {
            if (string.IsNullOrEmpty(_manger.Client.Browser.Options.AppInsightsKey)) throw new InvalidOperationException("The Application Insights key was not specified.  Please specify an Instrumentation key in the Browser Options.");
            properties.Add("ClientSessionId", _manger.Client.ClientSessionId.ToString());

            //var telemetry = new Microsoft.ApplicationInsights.TelemetryClient { InstrumentationKey = _manger.Client.Browser.Options.AppInsightsKey };

            var telemetryConfig = TelemetryConfiguration.CreateDefault();
            telemetryConfig.ConnectionString = _manger.Client.Browser.Options.AppInsightsKey; // Убедись, что это ConnectionString, а не InstrumentationKey
            var telemetry = new TelemetryClient(telemetryConfig);

            telemetry.TrackEvent(eventName, properties, metrics);
            telemetry.Flush();

            telemetry = null;
        }

        internal string GetRecentPageName()
        {
            return _manger.Client.Browser.Driver.FindElement(By.XPath("//div[@data-id='performance-widget']//div[contains(@style, '253, 253, 253')]/span[1]")).Text;
        }
        internal Dictionary<string, double> GetPerformanceMarkers(string page)
        {
            SelectPerformanceWidgetPage(page);

            var jsonResults = _manger.Client.Browser.Driver.ExecuteScript("return JSON.stringify(UCPerformanceTimeline.getKeyPerformanceIndicators())").ToString();
            var list = JsonConvert.DeserializeObject<List<KeyPerformanceIndicator>>(jsonResults);

            Dictionary<string, double> dict = list.ToDictionary(x => x.Name, x => x.Value);

            return dict;
        }

        internal Dictionary<string, string> GetMetadataMarkers()
        {
            Dictionary<string, object> jsonResults = (Dictionary<string, object>)_manger.Client.Browser.Driver.ExecuteScript("return UCPerformanceTimeline.getMetadata()");

            return jsonResults.ToDictionary(x => x.Key, x => x.Value.ToString());
        }

        internal Dictionary<string, string> GetBrowserTimings(BrowserEventType type, bool clearTimings = false)
        {
            var entries = (IEnumerable<object>)_manger.Client.Browser.Driver.ExecuteScript("return window.performance.getEntriesByType('" + type.ToString("g").ToLowerString() + "')");

            var results = new Dictionary<string, string>();

            foreach (var entry in entries)
            {
                var dict = (Dictionary<string, object>)entry;
                var todict = dict.ToDictionary(x => x.Key, x => x.Value.ToString());
                results = results.Merge(todict);
            }

            if (clearTimings) ClearResourceTimings();

            return results;
        }

        internal void ShowHidePerformanceWidget()
        {
            _manger.Client.Browser.Driver.ClickWhenAvailable(By.XPath(AppElements.Xpath[AppReference.PerformanceWidget.Container]));
        }
        internal void SelectPerformanceWidgetPage(string page)
        {
            _manger.Client.Browser.Driver.ClickWhenAvailable(By.XPath(AppElements.Xpath[AppReference.PerformanceWidget.Page].Replace("[NAME]", page)));
        }

        internal void ClearResourceTimings()
        {
            _manger.Client.Browser.Driver.ExecuteScript("return window.performance.clearResourceTimings();");
        }
    }
}
