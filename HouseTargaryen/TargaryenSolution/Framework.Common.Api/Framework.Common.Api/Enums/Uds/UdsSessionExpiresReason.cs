namespace Framework.Common.Api.Enums.Uds
{
    /// <summary>
    /// The uds session expires reason.
    /// </summary>
    public enum UdsSessionExpiresReason
    {
        /// <summary>
        /// Notset 
        /// </summary>
        Notset,

        /// <summary>
        /// Concurrent_Users
        /// </summary>
        ConcurrentUsers,

        /// <summary>
        /// Customer_Support 
        /// </summary>
        CustomerSupport,

        /// <summary>
        /// Sso_Token_Expired 
        /// </summary>
        SsoTokenExpired,

        /// <summary>
        /// User_Signed_Off status 
        /// </summary>
        UserSignedOff,

        /// <summary>
        /// User_Inactivity status 
        /// </summary>
        UserInactivity,

        /// <summary>
        /// No_Active_Browser status 
        /// </summary>
        NoActiveBrowser,

        /// <summary>
        /// Maintenance status 
        /// </summary>
        Maintenance,

        /// <summary>
        /// Incomplete_Signon status 
        /// </summary>
        IncompleteSignon,

        /// <summary>
        /// User_Throttling status 
        /// </summary>
        UserThrottling,

        /// <summary>
        /// Targeted_User status 
        /// </summary>
        TargetedUser,
    }
}