// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;

namespace TALXIS.TestKit.Selectors.Browser
{
    public class BrowserRecordingEvent
    {
        public string Event { get; set; }
        public Int32? KeyCode { get; set; }
        public DateTime? DateTime { get; set; }
        public string Id { get; set; }
        public string CssSelector { get; set; }
        public string ElementId { get; set; }
        public string XPathValue { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string IFRAME { get; set; }
    }
}
