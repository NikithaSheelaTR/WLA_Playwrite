namespace Framework.Common.UI.Products.Shared.Models.CustomPages
{
    /// <summary>
    ///Custom Page Model 
    /// </summary>
    public class CustomPageModel
    {
        /// <summary>
        /// Contact Name
        /// </summary>
        public string CustomPageName { get; set; }

        /// <summary>
        /// Contacts Count
        /// </summary>
        public int ContactsCount { get; set; }

        /// <summary>
        /// Is Page Editable
        /// </summary>
        public bool IsPageEditable { get; set; }
    }
}
