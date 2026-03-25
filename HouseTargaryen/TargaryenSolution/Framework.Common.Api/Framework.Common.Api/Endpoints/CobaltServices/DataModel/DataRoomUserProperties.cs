namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel
{
    /// <summary>
    /// Data Room User Properties
    /// Used for /DataRoom/v4/p.dataroom.{dataroomId}/settings/user/property/{propertyName} API calls
    /// </summary>
    public enum DataRoomUserProperties
    {
        /// <summary>
        /// Dockets Order Preferences
        /// </summary>
        DocketsOrderPreferences,

        /// <summary>
        /// Folder Analysis Keycite Baseline Status 
        /// </summary>
        FolderAnalysisKeyciteBaselineStatus,

        /// <summary>
        /// Folder Analysis Recodocs Baseline Status 
        /// </summary>
        FolderAnalysisRecodocsBaselineStatus,

        /// <summary>
        /// Send Runner Preferences
        /// </summary>
        SendRunnerPreferences
    }
}