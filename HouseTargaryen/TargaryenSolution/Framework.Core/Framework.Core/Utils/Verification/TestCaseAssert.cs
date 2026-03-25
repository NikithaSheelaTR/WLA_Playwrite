namespace Framework.Core.Utils.Verification
{
    using Framework.Core.QualityChecks.Result;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test Case Assert
    /// </summary>
    public class TestCaseAssert : BaseTestCase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseAssert"/> class. 
        /// </summary>
        /// <param name="handle">
        /// For API tests TakeScreenshotDelegate is null
        /// for UI - TakeScreenshot method 
        /// </param>
        public TestCaseAssert(TakeScreenshotDelegate handle = null) : base(handle)
        {
        }

        /// <summary>
        /// Clean Up Test Case Assert
        /// </summary>
        public void CleanUp()
        {
            this.WriteLogs("TestCaseAssert");
        }

        /// <summary>
        /// The handle exception.
        /// </summary>
        /// <param name="check">
        /// The check.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// </exception>
        protected override void HandleException(string check, AssertFailedException ex)
        {
            this.ScreenshotTaker?.Invoke();
            this.Checks[check] = Outcome.Failed;
            Logger.LogError($"Assert | Failed | {check} | {ex.Message}");
            throw ex;
        }

        /// <summary>
        /// The handle passed verification.
        /// </summary>
        /// <param name="check">
        /// The check.
        /// </param>
        protected override void HandlePassedVerification(string check)
        {
            this.Checks[check] = Outcome.Passed;
            Logger.LogInfo($"Assert | Passed | {check} |");
        }
    }
}
