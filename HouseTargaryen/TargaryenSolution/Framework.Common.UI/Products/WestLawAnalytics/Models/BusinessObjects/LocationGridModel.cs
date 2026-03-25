namespace Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects
{
    /// <summary>
    /// Location Grid Model
    /// </summary>
    public class LocationGridModel
    {
        /// <summary>
        /// Get Location name
        /// </summary>
        /// <returns>Location name</returns>
        public string LocationName { get; set; }

        /// <summary>
        /// Is location displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsLocationDisplayed { get; set; }

        /// <summary>
        /// Is Client and Matter button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsClientAndMatterButtonDisplayed { get; set; }

        /// <summary>
        /// Is Reason Code button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsReasonCodeButtonDisplayed { get; set; }

        /// <summary>
        /// Is Practice Area button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsPracticeAreaButtonDisplayed { get; set; }

        /// <summary>
        /// Is Extract Current List button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsExtractCurrentListButtonDisplayed { get; set; }

        /// <summary>
        /// Is Delete button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsDeleteButtonDisplayed { get; set; }

        /// <summary>
        /// Is Add Location button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsEditLocationButtonDisplayed { get; set; }

        /// <summary>
        /// Is edit client validation button displayed
        /// </summary>
        /// <returns>True if element is displayed</returns>
        public bool IsEditClientValidationButtonDisplayed { get; set; }
    }
}
