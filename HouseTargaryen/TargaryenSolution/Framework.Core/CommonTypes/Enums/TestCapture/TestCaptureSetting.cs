namespace Framework.Core.CommonTypes.Enums.TestCapture
{
    /// <summary>
    /// Used to set the test capture setting for a test (meaning whether or not a screenshot or video is taken and when)
    /// </summary>
    public enum TestCaptureSetting
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Neither Screenshots nor a video recording will be captured
        /// </summary>
        NO_CAPTURE,

        /// <summary>
        /// Record a video of the test case
        /// ***WARNING - IF YOU USE THIS, PLEASE DO NOT KEEP MORE THAN A FEW DAYS WORTH OF RECORDINGS,
        /// AS THE RECORDINGS USE A SIGNIFICANT AMOUNT OF NAS SPACE***
        /// </summary>
        VIDEO_CAPTURE,

        /// <summary>
        /// Record a video of the test case only if it fails
        /// ***WARNING - IF YOU USE THIS, PLEASE DO NOT KEEP MORE THAN A FEW DAYS WORTH OF RECORDINGS,
        /// AS THE RECORDINGS USE A SIGNIFICANT AMOUNT OF NAS SPACE***
        /// </summary>
        VIDEO_CAPTURE_FAILED_TESTS_ONLY,

        /// <summary>
        /// Take a screenshot for each failed check
        /// </summary>
        SCREEN_CAPTURE_EACH_FAILED_CHECK,

        /// <summary>
        /// Take a screenshot for each failed verify
        /// </summary>
        SCREEN_CAPTURE_EACH_FAILED_VERIFY,

        /// <summary>
        /// Take a screenshot for each failed assert
        /// </summary>
        SCREEN_CAPTURE_EACH_FAILED_ASSERT,

        /// <summary>
        /// Take a screenshot of each check
        /// </summary>
        SCREEN_CAPTURE_EACH_CHECK,

        /// <summary>
        /// Take a screenshot of each verify
        /// </summary>
        SCREEN_CAPTURE_EACH_VERIFY,

        /// <summary>
        /// Take a screenshot of each assert
        /// </summary>
        SCREEN_CAPTURE_EACH_ASSERT,

        /// <summary>
        /// Take a screenshot of the first failed check
        /// </summary>
        SCREEN_CAPTURE_FIRST_FAILED_CHECK,

        /// <summary>
        /// Take a screenshot of the first failed verify
        /// </summary>
        SCREEN_CAPTURE_FIRST_FAILED_VERIFY,

        /// <summary>
        /// Take a screenshot of the first failed assert
        /// </summary>
        SCREEN_CAPTURE_FIRST_FAILED_ASSERT,

        /// <summary>
        /// Take a screenshot at the end of each test
        /// </summary>
        SCREEN_CAPTURE_END_OF_TEST,

        /// <summary>
        /// take a screenshot at the end of each failed test
        /// </summary>
        SCREEN_CAPTURE_END_OF_FAILED_TEST

        // ReSharper enable InconsistentNaming
    }
}
