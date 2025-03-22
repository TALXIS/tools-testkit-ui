namespace TALXIS.TestKit.Bindings.Steps
{
    using Reqnroll;
    using TALXIS.TestKit.Bindings;

    /// <summary>
    /// Steps providing various utilities.
    /// </summary>
    [Binding]
    public class UtilitySteps : PowerAppsStepDefiner
    {
        /// <summary>
        /// Waits for a given number of seconds.
        /// </summary>
        [When(@"I wait up to '(.*)' seconds")]
        public static void WhenIWaitUpToSeconds(int seconds)
        {
            XrmApp.ThinkTime(seconds * 1000);
        }
    }
}
