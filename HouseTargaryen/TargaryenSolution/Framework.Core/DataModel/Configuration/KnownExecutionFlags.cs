namespace Framework.Core.DataModel.Configuration
{
    using System;

    /// <summary>
    /// The well-known execution setting flags to control the execution flow 
    /// in test set-up and test clean-up routines in test controllers.
    /// </summary>
    [Flags]
    internal enum KnownExecutionFlags : uint
    {
        /// <summary>
        /// The none.
        /// </summary>
        None = 0,

        /// <summary>
        /// Enables user
        /// </summary>
        CredentialManagement = 0x00000001,

        /// <summary>
        /// Enables a Super Delete operation on test set-up.
        /// </summary>
        SuperDeleteOnSetUp = 0x00000002,

        /// <summary>
        /// Enables a Super Delete operation on test clean-up.
        /// </summary>
        SuperDeleteOnCleanUp = 0x00000004,

        /// <summary>
        /// Enables API preconditions.
        /// </summary>
        ApiPreconditions = 0x00000008,

        /// <summary>
        /// Enables API postconditions.
        /// </summary>
        ApiPostconditions = 0x00000010,

        /// <summary>
        /// Enables API precondition routines.
        /// </summary>
        ApiPreconditionRoutines = 0x00000020,

        /// <summary>
        /// Enables API postcondition routines.
        /// </summary>
        ApiPostconditionRoutines = 0x00000040,

        /// <summary>
        /// Enables automated sign-on to an application under test.
        /// </summary>
        AutoSignOn = 0x00000080,

        /// <summary>
        /// Enables automated sign-off from an application under test.
        /// </summary>
        AutoSignOff = 0x00000100,

        /// <summary>
        /// Enables UI preconditions.
        /// </summary>
        UiPreconditions = 0x00000200,

        /// <summary>
        /// Enables UI postconditions.
        /// </summary>
        UiPostconditions = 0x00000400,

        /// <summary>
        /// Enables UI precondition routines.
        /// </summary>
        UiPreconditionRoutines = 0x00000800,

        /// <summary>
        /// Enables UI postcondition routines.
        /// </summary>
        UiPostconditionRoutines = 0x00001000,

        /// <summary>
        /// Enables context logging on test set-up.
        /// </summary>
        ContextLoggingOnSetUp = 0x00002000,

        /// <summary>
        /// Enables context logging on test clean-up.
        /// </summary>
        ContextLoggingOnCleanUp = 0x00004000,

	    /// <summary>
	    /// The screenshot on quality check fail.
	    /// </summary>
	    ScreenshotOnQualityCheckFail = 0x00008000,

        /// <summary>
        /// The screenshot on quality check fail on the report portal.
        /// </summary>
        ScreenshotOnReportPortalWhenCheckFail = 0x00016000
    }
}