namespace Framework.Common.UI.DataModel
{
    using System;

    using Framework.Core.DataModel.Configuration;

    /// <summary>
    /// The execution setting options to control the execution flow 
    /// in test set-up and test clean-up routines in UI test controllers.
    /// </summary>
    [Flags]
    public enum UiExecutionFlags : uint
    {
        /// <summary>
        /// Allows none.
        /// </summary>
        AllowNone = 0,

        /// <summary>
        /// Allows credential management.
        /// </summary>
        AllowCredentialManagement = KnownExecutionFlags.CredentialManagement,

        #region API settings

        /// <summary>
        /// Allows super delete on set up.
        /// </summary>
        AllowSuperDeleteOnSetUp = KnownExecutionFlags.SuperDeleteOnSetUp,

        /// <summary>
        /// Allows super delete on clean up.
        /// </summary>
        AllowSuperDeleteOnCleanUp = KnownExecutionFlags.SuperDeleteOnCleanUp,

        /// <summary>
        /// Allows API preconditions.
        /// </summary>
        AllowApiPreconditions = KnownExecutionFlags.ApiPreconditions,

        /// <summary>
        /// Allows API post conditions.
        /// </summary>
        AllowApiPostconditions = KnownExecutionFlags.ApiPostconditions,

        /// <summary>
        /// Allows API precondition routines.
        /// </summary>
        AllowApiPreconditionRoutines = KnownExecutionFlags.ApiPreconditionRoutines,

        /// <summary>
        /// Allows API post condition routines.
        /// </summary>
        AllowApiPostconditionRoutines = KnownExecutionFlags.ApiPostconditionRoutines,
        #endregion

        #region UI settings

        /// <summary>
        /// Allows auto sign on.
        /// </summary>
        AllowAutoSignOn = KnownExecutionFlags.AutoSignOn,

        /// <summary>
        /// Allows auto sign off.
        /// </summary>
        AllowAutoSignOff = KnownExecutionFlags.AutoSignOff,

        /// <summary>
        /// Allows UI preconditions.
        /// </summary>
        AllowUiPreconditions = KnownExecutionFlags.UiPreconditions,

        /// <summary>
        /// Allows UI post conditions.
        /// </summary>
        AllowUiPostconditions = KnownExecutionFlags.UiPostconditions,

        /// <summary>
        /// Allows UI precondition routines.
        /// </summary>
        AllowUiPreconditionRoutines = KnownExecutionFlags.UiPreconditionRoutines,

        /// <summary>
        /// Allows UI post condition routines.
        /// </summary>
        AllowUiPostconditionRoutines = KnownExecutionFlags.UiPostconditionRoutines,
        #endregion

        #region Logging settings

        /// <summary>
        /// Allows context logging on set up.
        /// </summary>
        AllowContextLoggingOnSetUp = KnownExecutionFlags.ContextLoggingOnSetUp,

        /// <summary>
        /// Allows context logging on clean up.
        /// </summary>
        AllowContextLoggingOnCleanUp = KnownExecutionFlags.ContextLoggingOnCleanUp,

	    /// <summary>
	    /// To allow screenshot on failed quality check.
	    /// </summary>
	    AllowScreenshotOnFailedQualityCheck = KnownExecutionFlags.ScreenshotOnQualityCheckFail,

        /// <summary>
        /// To allow screenshot on failed quality check.
        /// </summary>
        AllowScreenshotOnFailedQualityCheckReportPortal = KnownExecutionFlags.ScreenshotOnReportPortalWhenCheckFail
        #endregion
    }
}