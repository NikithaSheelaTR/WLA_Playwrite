namespace Framework.Core.TestCapture
{
    using Framework.Core.CommonTypes.Enums.TestCapture;
    using Framework.Core.QualityChecks.Result;

    /// <summary>
    /// ScreenshotTaker class.
    /// Interface for classes used to handle taking screenshots, recording video, or otherwise capturing a test case.
    /// </summary>
    public interface IScreenshotTaker
    {
        #region Getter Functions

        /// <summary>
        /// Returns the default image file format for screenshots taken.
        /// </summary>
        /// <returns>The default image file format for screenshots taken.</returns>
        ImageFileType GetScreenshotFormat();

        /// <summary>
        /// Returns the maximum amount of time the video recording should run for. The TimeUnit for this is determined by the getTimeUnit() function.
        /// </summary>
        /// <returns>The maximum amount of time the video recording should run for.</returns>
        int GetVideoTimeout();

        /// <summary>
        /// Returns the default video screenshot rate. This number determines how often screenshots are taken.
        /// The TimeUnit for this is determined by the getTimeUnit() function.
        /// For example, if the TimeUnit is seconds, the screenshot rate is 1, and the frame rate is 2, 
        /// a screenshot will be taken every second with each of those screenshots being displayed in the video for 2 seconds.
        /// </summary>
        /// <returns></returns>
        int GetVideoScreenshotRate();

        /// <summary>
        /// Returns the default video screenshot rate. This number determines how often screenshots are taken.
        /// The TimeUnit for this is determined by the getTimeUnit() function.
        /// For example, if the TimeUnit is seconds, the screenshot rate is 1, and the frame rate is 2, 
        /// a screenshot will be taken every second with each of those screenshots being displayed in the video for 2 seconds.
        /// </summary>
        /// <returns>The default video screenshot rate.</returns>
        int GetVideoFrameRate();

        /// <summary>
        ///  Returns the default file location of the video being recorded.
        /// </summary>
        /// <returns>The default file location of the video being recorded.</returns>
        string GetDefaultVideoFile();

        #endregion

        #region Screenshot Functionality 

        /// <summary>
        /// Takes a screenshot and saves it to a file in the specified directory with the specified filename.
        /// The file extension will be the default file extension.
        /// </summary>
        /// <param name="directory">The directory where the screenshot should go.</param>
        /// <param name="fileName">The filename of the screenshot.</param>
        /// <param name="qrtScreenshotPath">the reference path used by QRT when determining where to
        /// point to when generating links to the screenshot (as it must point to the NAS not where the
        /// screenshot is saved locally)</param>
        /// <returns>The absolute file path of the newly-taken screenshot.</returns>
        string TakeScreenshot(string directory, string fileName, string qrtScreenshotPath);

        #endregion

        #region Video Recording Functionality 

        /// <summary>
        /// Sets the default video file where video the recordings go.
        /// </summary>
        /// <param name="defaultVideoDir">The default video directory where the video recordings should go.</param>
        /// <param name="defaultVideoFileName">The default video file name where the video recordings should be saved. This DOES NOT include the file extension.</param>
        void SetDefaultVideoFile(string defaultVideoDir, string defaultVideoFileName);

        /// <summary>
        /// Sets the default video file where video the recordings go.
        /// </summary>
        /// <param name="defaultVideoFile">The default video file where video the recordings go.</param>
        void SetDefaultVideoFile(string defaultVideoFile);

        /// <summary>
        /// Checks to see if a video recording has been started.
        /// </summary>
        /// <returns>True if a video recording has been started and false if not.</returns>
        bool IsVideoRecordingStarted();

        /// <summary>
        /// Starts a video recording and saves the MP4 file to the specified File location when it's finished.
        /// </summary>
        /// <param name="videoLocation">The File location where the video should be saved.</param>
        /// <returns>Returns a copy of the RobotUtils for chaining purposes.</returns>
        IScreenshotTaker StartVideoRecording(string videoLocation);

        /// <summary>
        /// Stops the video recording that is running.
        /// </summary>
        /// <returns>Returns a copy of the object for chaining purposes.</returns>
        IScreenshotTaker StopVideoRecording(bool keepVideo);
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qualityTestCase"></param>
        void SetQualityTestCase(QualityTestCase qualityTestCase);
    }
}
