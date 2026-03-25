namespace Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// International Tab Panel
    /// </summary>
    public class InternationalTabPanel : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='Browse-widget']//div[contains(@class,'Tab-panel') and @aria-hidden='false']");
        private static readonly By RegionNamesLocator = By.XPath(".//div[@class='Tab-panel Tab-panel--show']//span[text()='External Link']/preceding-sibling::span");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "International";

        /// <summary>
        /// Region Names
        /// </summary>
        public IReadOnlyCollection<ILabel> RegionNames => new ElementsCollection<Label>(this.ComponentLocator, RegionNamesLocator);
    }
}
