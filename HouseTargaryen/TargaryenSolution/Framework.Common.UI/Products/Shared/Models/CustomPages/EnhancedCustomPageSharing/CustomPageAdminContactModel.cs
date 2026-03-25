namespace Framework.Common.UI.Products.Shared.Models.CustomPages.EnhancedCustomPageSharing
{
    /// <summary>
    /// The custom page contact model.
    /// </summary>
    public class CustomPageAdminContactModel
    {
        /// <summary>
        /// Gets or sets the Contact name.
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Gets or sets Role.
        /// </summary>
        public string ActiveRole { get; set; }

        /// <summary>
        /// Is User Active
        /// </summary>
        public bool IsUserActive { get; set; }

        /// <summary>
        /// Is Inactive Badge Displyed
        /// </summary>
        public bool IsInactiveBadgeDisplayed { get; set; }

        /// <summary>
        /// Is User disabled
        /// </summary>
        public bool IsUserDisabled { get; set; }
    }
}
