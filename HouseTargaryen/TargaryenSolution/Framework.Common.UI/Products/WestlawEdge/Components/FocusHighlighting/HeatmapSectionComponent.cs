namespace Framework.Common.UI.Products.WestlawEdge.Components.FocusHighlighting
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Models.FocusHighlighting;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Heatmap section component
    /// </summary>
    public class HeatmapSectionComponent : BaseItem
    {
        private static readonly By TocEntryContentLinkLocator = By.XPath(".//ancestor::div[@class = 'TocEntryContent']//a");
        private static readonly By HeatmapColorLocator = By.XPath(".//div[contains(@class, 'HeatMapColor')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="HeatmapSectionComponent"/> class. 
        /// The constructor
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public HeatmapSectionComponent(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// The term color map.
        /// </summary>
        private EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> termColorMap;

        /// <summary>
        /// Gets the TermColors enumeration to FocusHighlightingTermInfo map.
        /// </summary>
        protected EnumPropertyMapper<TermColors, FocusHighlightingTermInfo> TermColorMap =>
            this.termColorMap = this.termColorMap
                                ?? EnumPropertyModelCache.GetMap<TermColors, FocusHighlightingTermInfo>(
                                    string.Empty,
                                    @"Resources/EnumPropertyMaps/WestlawEdge/FocusHighlighting");

        /// <summary>
        /// Title
        /// </summary>
        public string Title => DriverExtensions.GetElement(this.Container, TocEntryContentLinkLocator).GetAttribute("title");

        /// <summary>
        /// Hover heatmap color
        /// </summary>
        /// <param name="color">Term color</param>
        /// <returns>New instance of the HeatmapHoverBoxDialog</returns>
        public HeatmapHoverBoxDialog HoverHeatmapColor(TermColors color)
        {
            DriverExtensions.GetElements(this.Container, HeatmapColorLocator)
                            .First(
                                x => x.GetCssValue("background-color").Equals(this.TermColorMap[color].BackgroundColorCode))
                            .SeleniumHover();

            return new HeatmapHoverBoxDialog();
        }

        /// <summary>
        /// Click heatmap color
        /// </summary>
        /// <param name="color">Term color</param>
        /// <returns>New instance of the HeatmapHoverBoxDialog</returns>
        public void ClickHeatmapColor(TermColors color) =>
            DriverExtensions.GetElements(this.Container, HeatmapColorLocator).
            First(x => x.GetCssValue("background-color").Equals(this.TermColorMap[color].BackgroundColorCode)).Click();
    }
}