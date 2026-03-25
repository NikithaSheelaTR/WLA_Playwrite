namespace Framework.Common.UI.Utils.Browser
{
    using System;
    using System.Drawing;
    using System.IO;

    using Framework.Core.QualityChecks.Result;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Windows;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;

    /// <summary>
    /// ScreenshotTaker
    /// </summary>
    public class ScreenshotTaker
    {
        private readonly QualityTestCase qualityTestCase;

        private readonly TestContext testContext;

        private int threadId;

        private bool allowScreenshotWithMultipleThreads;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenshotTaker"/> class. 
        /// </summary>
        /// <param name="testContext">The test Context.</param>
        /// <param name="qualityTestCase">The quality test case context.</param>
        /// <param name="threadId">The thread Id.</param>
        /// <param name="allowScreenshotWithMultipleThreads"></param>
        public ScreenshotTaker(TestContext testContext, QualityTestCase qualityTestCase, int threadId, bool allowScreenshotWithMultipleThreads = false)
        {
            this.testContext = testContext;
            this.qualityTestCase = qualityTestCase;
            this.threadId = threadId;
            this.allowScreenshotWithMultipleThreads = allowScreenshotWithMultipleThreads;

            if (this.testContext == null)
            {
                Logger.LogError(
                    $"{nameof(ScreenshotTaker)} will not take a screenshot, because {nameof(this.testContext)} is NULL");
            }

            if (this.qualityTestCase == null)
            {
                Logger.LogError(
                    $"{nameof(ScreenshotTaker)} will not take a screenshot, because {nameof(this.qualityTestCase)} is NULL");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenshotTaker"/> class. 
        /// </summary>
        /// <param name="testContext">The test Context.</param>
        /// <param name="threadId">The thread Id.</param>
        /// <param name="allowScreenshotWithMultipleThreads">allow report portal screenshot</param>
        public ScreenshotTaker(TestContext testContext, int threadId, bool allowScreenshotWithMultipleThreads = false)
        {
            this.testContext = testContext;
            this.threadId = threadId;
            this.allowScreenshotWithMultipleThreads = allowScreenshotWithMultipleThreads;

            if (this.testContext == null)
            {
                Logger.LogError(
                    $"{nameof(ScreenshotTaker)} will not take a screenshot, because {nameof(this.testContext)} is NULL");
            }
        }

        /// <summary>
        /// Create screenshot
        /// </summary>
        public void TakeScreenshot()
        {
            if (this.testContext == null || this.qualityTestCase == null)
            {
                return;
            }

            string fileName = $"{this.testContext.TestName}{DateTime.Now:yyyy-MM-dd_HH-mm-ss.ff}.jpg";
            string relativeFilePath = Path.Combine(new DirectoryInfo(this.testContext.TestRunDirectory).Name, fileName);
            string fullFilePath = Path.Combine(this.testContext.TestRunDirectory, fileName);

            SafeMethodExecutor.Execute(
                () =>
                    {
                        // We shall place the physical file in the "Out" folder's parent on disk.
                        BrowserPool.GetBrowserByThreadId(this.threadId).TakeScreenshot(
                            fullFilePath,
                            ScreenshotImageFormat.Jpeg);
                        this.testContext.AddResultFile(fullFilePath);

                        // We shall only record the relative path to the screenshot in the QualityTestCase
                        // to help QRT to resolve the link properly.
                        this.qualityTestCase.Attachments.Add(relativeFilePath);
                    }).LogDetails();
        }

        /// <summary>
        /// Create screen shot without any dependency on QRT
        /// </summary>
        public void TakeScreenshotRp()
        {
            if (this.testContext == null)
            {
                return;
            }

            string fileName = $"{this.testContext.TestName}{DateTime.Now:yyyy-MM-dd_HH-mm-ss.ff}.jpg";
            string fullFilePath = Path.Combine(this.testContext.TestRunDirectory, fileName);
            string relativeFilePath = Path.Combine(new DirectoryInfo(this.testContext.TestRunDirectory).Name, fileName);

            SafeMethodExecutor.Execute(
                () =>
                    {
                        // We shall place the physical file in the "Out" folder's parent on disk.
                        BrowserPool.GetBrowserByThreadId(this.threadId).TakeScreenshot(
                            fullFilePath,
                            ScreenshotImageFormat.Jpeg);
                        this.testContext.AddResultFile(fullFilePath);
                    }).LogDetails();

            if (this.allowScreenshotWithMultipleThreads)
            {
                string screenshot = Convert.ToBase64String(new Bitmap(fullFilePath).BitmapToArray());
                Logger.LogInfo("Screenshot: {rp#base64#image/png#" + screenshot + "}");
            }
            else
            {
                Logger.LogInfo(relativeFilePath);
            }

        }

        /// <summary>
        /// Takes a screenshot if a quality check failed.
        /// </summary>
        /// <param name="qualityCheck">The quality check.</param>
        public void TakeScreenshotOnFailure(QualityCheck qualityCheck)
        {
            if (qualityCheck != null && qualityCheck.Outcome == Outcome.Failed)
            {
                this.TakeScreenshot();
            }
        }
    }
}