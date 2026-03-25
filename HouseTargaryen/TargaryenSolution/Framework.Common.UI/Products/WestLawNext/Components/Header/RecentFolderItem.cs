namespace Framework.Common.UI.Products.WestLawNext.Components.Header
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Recent Folder Item.
    /// </summary>
    public class RecentFolderItem : BaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentFolderItem"/> class. 
        /// </summary>
        /// <param name="containerElement">
        /// The container Element.
        /// </param>
        public RecentFolderItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// The get link text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string LinkFolderName => this.Container.GetText();
        
        /// <summary>
        /// The hover.
        /// </summary>
        public void Hover() => this.Container.SeleniumHover();

        /// <summary>
        /// Click on the folder
        /// </summary>
        public void Click() => this.Container.Click();

        /// <summary>
        /// The check highlighted color.
        /// </summary>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsHighlightedColorExpected(string color)
            => this.Container.GetCssValue("background-color").Equals(color);
    }
}