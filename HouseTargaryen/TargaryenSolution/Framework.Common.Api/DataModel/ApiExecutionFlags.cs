namespace Framework.Common.Api.DataModel
{
    using System;

    using Framework.Core.DataModel.Configuration;

    /// <summary>
    /// The api execution flags.
    /// </summary>
    [Flags]
    public enum ApiExecutionFlags : uint
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

        /// <summary>
        /// Allows Api session
        /// </summary>
        AllowAutoSignOn = KnownExecutionFlags.AutoSignOn,

        /// <summary>
        /// Allows Api sing Off
        /// </summary>
        AllowAutoSignOff = KnownExecutionFlags.AutoSignOff,
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

        #endregion
    }
}