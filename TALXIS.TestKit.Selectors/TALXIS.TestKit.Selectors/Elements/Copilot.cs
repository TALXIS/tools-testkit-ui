// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using TALXIS.TestKit.Selectors.WebClientManagement;
using TALXIS.TestKit.Selectors.Browser;

namespace TALXIS.TestKit.Selectors
{
    public class CustomerServiceCopilot : Element
    {
        private readonly PowerAppManager _powerAppManager;
        private readonly CommandBarManager _commandBarManager;

        public CustomerServiceCopilot(WebClient client) : base()
        {
            _powerAppManager = new PowerAppManager(client);
            _commandBarManager = new CommandBarManager(client);
        }

        /// <summary>
        /// Clicks command on the command bar
        /// </summary>
        /// <param name="name">Name of button to click</param>
        /// <param name="subname">Name of button on submenu to click</param>
        /// <param name="subSecondName">Name of button on submenu (3rd level) to click</param>
        public void ClickButton(string name, string subname = null, string subSecondName = null)
        {
            _commandBarManager.ClickCommand(name, subname, subSecondName);
        }

        public BrowserCommandResult<bool> EnableAskAQuestion(int thinkTime = Constants.DefaultThinkTime)
        {
            return _powerAppManager.EnableAskAQuestion();
        }

        public BrowserCommandResult<string> AskAQuestion(string userInput, int thinkTime = Constants.DefaultThinkTime)
        {
            return _powerAppManager.AskAQuestion(userInput);
        }
    }
}
