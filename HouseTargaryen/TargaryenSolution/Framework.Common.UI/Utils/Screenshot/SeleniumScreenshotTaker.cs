namespace Framework.Common.UI.Utils.Screenshot
{
    using System;
    using System.IO;

    using Framework.Core.CommonTypes.Enums.TestCapture;
    using Framework.Core.CommonTypes.Settings;
    using Framework.Core.QualityChecks.Result;
    using Framework.Core.TestCapture;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;

    /// <summary>
    /// SeleniumScreenshotTaker singleton class used to take screenshots
    /// </summary>
    /// <remarks>
    /// Note: if you are using the BasePage and BaseTest classes you'll have references to this singleton
    /// </remarks>
    public class SeleniumScreenshotTaker : IScreenshotTaker
    {
        #region ScreenshotTaker Private Instance Variables

        private static ImageFileType screenshotFormat = ImageFileType.JPEG;

        /// <summary>
        /// private reference to TestContext - needed so we can add a result file
        /// </summary>
        private TestContext TestContext { get; set; }

        #endregion

        #region Public Variables

        /// <summary>
        /// Directory to place the screenshots - default will be set to 
        /// TestContext.TestRunDirectory
        /// </summary>
        public string ScreenshotDirectory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TestSettings TestSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IWebDriver Driver { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public QualityTestCase QualityTestCase { get; set; }

        #endregion

        #region ScreenshotTaker Getter Functions 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ImageFileType GetScreenshotFormat()
        {
            return screenshotFormat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GetVideoTimeout()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GetVideoScreenshotRate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int GetVideoFrameRate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetDefaultVideoFile()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ScreenshotTaker Setter Functions 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityTestCase"></param>
        public void SetQualityTestCase(QualityTestCase qualityTestCase)
        {
            this.QualityTestCase = qualityTestCase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFileType"></param>
        public void SetScreenshotFormat(ImageFileType imageFileType)
        {
            screenshotFormat = imageFileType;
        }

        #endregion

        #region ScreenshotTaker Functionality 

        /// <summary>
        /// Takes a screenshot using the Driver object and adds the file to the result files in the TestContext object
        /// Has a limit of 25 screenshots per testmethod 
        /// </summary>
        /// <param name="directory"></param> 
        /// <param name="filename"></param>
        /// <param name="qrtScreenshotPath">used to specify the referential path to include in the QualityTestCase object
        /// so that when the results are loaded to QRT, the sc</param>
        public string TakeScreenshot(string directory = null, string filename = null, string qrtScreenshotPath = null)
        {
            try
            {
                string driverName =
                    this.Driver.ToString().Substring(this.Driver.ToString().LastIndexOf(".", StringComparison.Ordinal) + 1);

                OpenQA.Selenium.Screenshot ss = ((ITakesScreenshot)this.Driver).GetScreenshot();

                // if filename is null, set it to the testname
                filename = filename ?? this.TestContext.TestName;

                // if filename is null, set it to the testname
                directory = directory ?? this.TestContext.TestRunDirectory;

                qrtScreenshotPath = qrtScreenshotPath ?? "";

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // create the jpeg filename path
                string filepath = directory + "\\" + filename;

                Console.WriteLine("Saving screenshot at: " + filepath);

                // save the file and add it to the result files - so there's a reference in the trx file
                ss.SaveAsFile(filepath, screenshotFormat.GetScreenshotImageFormat());
                this.QualityTestCase.Attachments.Add(qrtScreenshotPath + "\\" + filename);
                return filepath;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultVideoDir"></param>
        /// <param name="defaultVideoFileName"></param>
        public void SetDefaultVideoFile(string defaultVideoDir, string defaultVideoFileName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultVideoFile"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetDefaultVideoFile(string defaultVideoFile)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsVideoRecordingStarted()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IScreenshotTaker StartVideoRecording()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoLocation"></param>
        /// <returns></returns>
        public IScreenshotTaker StartVideoRecording(string videoLocation)
        {
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keepVideo"></param>
        /// <returns></returns>
        public IScreenshotTaker StopVideoRecording(bool keepVideo)
        {
            return this;
        }
    }
}
