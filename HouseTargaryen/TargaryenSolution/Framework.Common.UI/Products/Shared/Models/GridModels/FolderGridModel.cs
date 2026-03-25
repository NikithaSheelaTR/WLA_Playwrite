namespace Framework.Common.UI.Products.Shared.Models.GridModels
{
    using Framework.Common.UI.Products.Shared.Enums.Foldering;

    /// <summary>
    /// contains foldering and base grid properties
    /// </summary>
    public class FolderGridModel : GridObjectModel
    {
        /// <summary>
        /// True if Checkbox selected, false otherwise
        /// </summary>
        public bool Checkbox { get; set; }

        /// <summary>
        /// Gets the text of 'Content' section
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Get the text of 'Folder' section
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Get the type of and grid: document or folder
        /// </summary>
        public GridType Type { get; set; }
    }
}
