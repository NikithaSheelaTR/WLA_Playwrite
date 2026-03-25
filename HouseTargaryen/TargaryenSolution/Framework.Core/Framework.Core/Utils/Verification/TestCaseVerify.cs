namespace Framework.Core.Utils.Verification
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Framework.Core.QualityChecks.Result;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test Case Verify
    /// </summary>
    public class TestCaseVerify : BaseTestCase
    {
        private List<AssertFailedException> failedChecksDetails = new List<AssertFailedException>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseVerify"/> class. 
        /// </summary>
        /// <param name="handle">
        /// For API tests TakeScreenshotDelegate is null
        /// for UI - TakeScreenshot method </param>
        public TestCaseVerify(TakeScreenshotDelegate handle = null)
            : base(handle)
        {
        }

        /// <summary>
        /// TestResult
        /// </summary>
        public bool TestResult { get; set; } = true;

        #region CleanUp

        /// <summary>
        /// Fail test if outcome is False
        /// </summary>
        public void CleanUp()
        {
            this.WriteLogs("TestCaseVerify");

            if (!this.TestResult)
            {
                IEnumerable<string> outputMessages = this.failedChecksDetails.Select(el => el.Message);
                var finalMessage = new StringBuilder();
                foreach (var message in outputMessages)
                {
                    finalMessage.AppendLine("\r\n" + message);
                }
                Assert.Fail(finalMessage.ToString());
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// The handle exception.
        /// </summary>
        /// <param name="check">
        /// The check.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        protected override void HandleException(string check, AssertFailedException ex)
        {
            this.ScreenshotTaker?.Invoke();
            this.TestResult = false;
            this.Checks[check] = Outcome.Failed;
            this.failedChecksDetails.Add(ex);
            Logger.LogError($"Verification | Failed | {check} | {ex.Message}");
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
            Logger.LogInfo($"Verification | Passed | {check} |");
        }
        #endregion
    }
}
