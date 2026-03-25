namespace Framework.Common.UI.Products.WestLawAnalytics.Enums
{
    /// <summary>
    /// Client Validation Settings options
    /// </summary>
    public enum AnalyticsClientValidationOptions
    {
        /// <summary>
        /// Allow attorneys to submit a Research Description
        /// </summary>
        AllowAttorneysSubmitResearch,

        /// <summary>
        /// Allow attorneys to mark research as "Chargeable to Client" or "Not Chargeable to Client"
        /// </summary>
        AllowAttorneysToMarkResearch,

        /// <summary>
        /// Allow Users to Modify
        /// </summary>
        AllowUsersToModify,

        /// <summary>
        /// Enable Practice Area Designations for attorneys
        /// </summary>
        EnablePracticeAreaDesignations,

        /// <summary>
        /// Enable the use of Clients and Matters uploaded in Analytics
        /// </summary>
        EnableUploadedClientMatters,

        /// <summary>
        /// Suspend Client Validation
        /// </summary>
        SuspendClientValidation
    }
}
