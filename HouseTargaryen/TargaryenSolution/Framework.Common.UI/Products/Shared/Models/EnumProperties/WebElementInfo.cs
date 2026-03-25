namespace Framework.Common.UI.Products.Shared.Models.EnumProperties
{
    using Framework.Core.DataModel;

    /// <summary>
    /// The enumeration info.
    /// </summary>
    public class WebElementInfo : BaseTextModel
    {
        /// <summary>
        /// Gets or sets the class name.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the locator string.
        /// </summary>
        public string LocatorString { get; set; }

        /// <summary>
        /// Gets or sets the source file name.
        /// </summary>
        public string SourceFile { get; set; }

        /// <summary>
        /// Gets or sets the tag name.
        /// </summary>
        public string TagName { get; set; }
        

        /// <summary>
        /// Gets or sets locator mask
        /// </summary>
        public string LocatorMask { get; set; }
    }
}