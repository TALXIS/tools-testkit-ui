using System;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors.WebClientManagement.Helpers
{
    internal class PerformanceCenterManger
    {
        public WebClient Client { get; }


        public PerformanceCenterManger(WebClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        internal void EnablePerformanceCenter()
        {
            Client.Browser.Driver.Navigate().GoToUrl($"{Client.Browser.Driver.Url}&perf=true");
            Client.Browser.Driver.WaitForPageToLoad();
            Client.Browser.Driver.WaitForTransaction();
        }
    }
}
