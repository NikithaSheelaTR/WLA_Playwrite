namespace Framework.Common.UI.DataModel
{
    using System;

    /// <summary>
    /// The possible combinations of the execution setting options to control the execution flow 
    /// in test set-up and test clean-up routines in UI test controllers.
    /// </summary>
    [Flags]
    internal enum UiFlagDependencyMap : uint
    {
        /// <summary>
        /// Allows none.
        /// </summary>
        AllowNone = 0,

        /// <summary>
        /// Allows credential management.
        /// </summary>
        AllowCredentialManagement = UiExecutionFlags.AllowCredentialManagement,

        #region API settings

        /// <summary>
        /// Allows super delete on set up.
        /// </summary>
        AllowSuperDeleteOnSetUp = UiExecutionFlags.AllowApiPreconditions | UiExecutionFlags.AllowSuperDeleteOnSetUp,

        /// <summary>
        /// Allows super delete on clean up.
        /// </summary>
        AllowSuperDeleteOnCleanUp = UiExecutionFlags.AllowApiPostconditions | UiExecutionFlags.AllowSuperDeleteOnCleanUp,

        /// <summary>
        /// Allows API preconditions.
        /// </summary>
        AllowApiPreconditions = UiExecutionFlags.AllowApiPreconditions,

        /// <summary>
        /// Allows API post conditions.
        /// </summary>
        AllowApiPostconditions = UiExecutionFlags.AllowApiPostconditions,

        /// <summary>
        /// Allows API precondition routines.
        /// </summary>
        AllowApiPreconditionRoutines =
            UiExecutionFlags.AllowApiPreconditions | UiExecutionFlags.AllowApiPreconditionRoutines,

        /// <summary>
        /// Allows API post condition routines.
        /// </summary>
        AllowApiPostconditionRoutines =
            UiExecutionFlags.AllowApiPostconditions | UiExecutionFlags.AllowApiPostconditionRoutines,
        #endregion

        #region UI settings

        /// <summary>
        /// Allows auto sign on.
        /// </summary>
        AllowAutoSignOn =
            UiExecutionFlags.AllowCredentialManagement | UiExecutionFlags.AllowUiPreconditions
            | UiExecutionFlags.AllowAutoSignOn,

        /// <summary>
        /// Allows auto sign off.
        /// </summary>
        AllowAutoSignOff = UiExecutionFlags.AllowUiPostconditions | UiExecutionFlags.AllowAutoSignOff,

        /// <summary>
        /// Allows UI preconditions.
        /// </summary>
        AllowUiPreconditions = UiExecutionFlags.AllowUiPreconditions,

        /// <summary>
        /// Allows UI post conditions.
        /// </summary>
        AllowUiPostconditions = UiExecutionFlags.AllowUiPostconditions,

        /// <summary>
        /// Allows UI precondition routines.
        /// </summary>
        AllowUiPreconditionRoutines =
            UiExecutionFlags.AllowUiPreconditions | UiExecutionFlags.AllowUiPreconditionRoutines,

        /// <summary>
        /// Allows UI post condition routines.
        /// </summary>
        AllowUiPostconditionRoutines =
            UiExecutionFlags.AllowUiPostconditions | UiExecutionFlags.AllowUiPostconditionRoutines,
        #endregion

        #region Logging settings

        /// <summary>
        /// Allows context logging on set up.
        /// </summary>
        AllowContextLoggingOnSetUp = UiExecutionFlags.AllowContextLoggingOnSetUp,

        /// <summary>
        /// Allows context logging on clean up.
        /// </summary>
        AllowContextLoggingOnCleanUp = UiExecutionFlags.AllowContextLoggingOnCleanUp,

	    /// <summary>
	    /// To allow screenshot on failed quality check.
	    /// </summary>
	    AllowScreenshotOnFailedQualityCheck = UiExecutionFlags.AllowScreenshotOnFailedQualityCheck
		#endregion
	}
}