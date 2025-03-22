// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using TALXIS.TestKit.Selectors.WebClientManagement;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors
{
    public class PowerApp : Element
    {
        public enum AppType
        {
            EmbeddedPowerApp,
            CustomPage
        }

        private readonly PowerAppManager _client;

        public PowerApp(WebClient client) : base()
        {
            _client = new PowerAppManager(client);
        }

        /// <summary>
        /// Sends Power FX command to Power App
        /// </summary>
        /// <param name="appId">Id of the app to select</param>
        /// <param name="command">command to execute</param>
        public void SendCommand(string appId, string command)
        {
            _client.PowerAppSendCommand(appId, command);
        }

        public void Select(string appId, string control)
        {
            _client.PowerAppSelect(appId, control);
        }

        public void SetProperty(string appId, string control, string value)
        {
            _client.PowerAppSetProperty(appId, control, value);
        }

    }
}
