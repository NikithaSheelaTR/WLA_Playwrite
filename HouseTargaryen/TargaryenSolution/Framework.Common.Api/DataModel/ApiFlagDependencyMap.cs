namespace Framework.Common.Api.DataModel
{
    using System;

    [Flags]
    internal enum ApiFlagDependencyMap : uint
    {
        /// <summary>
        /// Allows none.
        /// </summary>
        AllowNone = 0,

        /// <summary>
        /// Allows credential management.
        /// </summary>
        AllowCredentialManagement = ApiExecutionFlags.AllowCredentialManagement,

        /// <summary>
        /// Allows auto sign on.
        /// </summary>
        AllowAutoSignOn =
            ApiExecutionFlags.AllowCredentialManagement | ApiExecutionFlags.AllowAutoSignOn,

        /// <summary>
        /// Allows auto sign off.
        /// </summary>
        AllowAutoSignOff = ApiExecutionFlags.AllowApiPostconditions | ApiExecutionFlags.AllowAutoSignOff,


        #region API settings

        /// <summary>
        /// Allows super delete on set up.
        /// </summary>
        AllowSuperDeleteOnSetUp = ApiExecutionFlags.AllowApiPreconditions | ApiExecutionFlags.AllowSuperDeleteOnSetUp,

        /// <summary>
        /// Allows super delete on clean up.
        /// </summary>
        AllowSuperDeleteOnCleanUp = ApiExecutionFlags.AllowApiPostconditions | ApiExecutionFlags.AllowSuperDeleteOnCleanUp,

        /// <summary>
        /// Allows API preconditions.
        /// </summary>
        AllowApiPreconditions = ApiExecutionFlags.AllowApiPreconditions,

        /// <summary>
        /// Allows API post conditions.
        /// </summary>
        AllowApiPostconditions = ApiExecutionFlags.AllowApiPostconditions,

        /// <summary>
        /// Allows API precondition routines.
        /// </summary>
        AllowApiPreconditionRoutines =
            ApiExecutionFlags.AllowApiPreconditions | ApiExecutionFlags.AllowApiPreconditionRoutines,

        /// <summary>
        /// Allows API post condition routines.
        /// </summary>
        AllowApiPostconditionRoutines =
            ApiExecutionFlags.AllowApiPostconditions | ApiExecutionFlags.AllowApiPostconditionRoutines,

        #endregion

        #region Logging settings

        /// <summary>
        /// Allows context logging on set up.
        /// </summary>
        AllowContextLoggingOnSetUp = ApiExecutionFlags.AllowContextLoggingOnSetUp,

        /// <summary>
        /// Allows context logging on clean up.
        /// </summary>
        AllowContextLoggingOnCleanUp = ApiExecutionFlags.AllowContextLoggingOnCleanUp
        #endregion
    }
}