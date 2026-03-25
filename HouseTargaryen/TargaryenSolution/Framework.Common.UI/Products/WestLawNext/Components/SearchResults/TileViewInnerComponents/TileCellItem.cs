namespace Framework.Common.UI.Products.WestLawNext.Components.SearchResults.TileViewInnerComponents
{
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components.SearchResults.TableViewInnerComponents;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Tile Cell Component
    /// </summary>
    public class TileCellItem : BaseItem
	{
        private static readonly By DocIconsLocator = By.XPath(".//ul[@class='co_documentIcons']//li");

        /// <inheritdoc />
        public TileCellItem(IWebElement containerWebElement)
            : base(containerWebElement)
        {
            this.TitleComponent = new TitleComponent(containerWebElement);
            this.ImageComponent = new ImageComponent(containerWebElement);
        }

        /// <summary>
        /// Gets the DocumentIcon enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<DocumentIcon, WebElementInfo> DocIconsMap { get; } = EnumPropertyModelCache.GetMap<DocumentIcon, WebElementInfo>();

        /// <summary>
        /// Title Component
        /// </summary>
        public TitleComponent TitleComponent { get;  }

		/// <summary>
		/// Trademark image component
		/// </summary>
		public ImageComponent ImageComponent { get; }

        /// <summary>
        /// Returns document icons
        /// </summary>
        /// <returns></returns>
        public List<DocumentIcon> GetDocumentIconsList()
        {
            IEnumerable<string> pathsToSrc = DriverExtensions
                .GetElements(this.Container, DocIconsLocator)
                .Where(elem => elem.Displayed)
                .Select(elem => elem.GetAttribute("class"));

            return pathsToSrc.Any()
                      ? this.DocIconsMap.Where(
                                pair => pathsToSrc.Any(path => pair.Value.LocatorString.Contains(path)))
                            .Select(pair => pair.Key).ToList()
                      : new List<DocumentIcon>();
        }
	}
}