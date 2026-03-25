namespace Framework.Common.UI.Products.Shared.Components.RecentResearch
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The recent research pane on the Client Id page
    /// </summary>
    public class RecentResearchComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.CssSelector("#co_clientIdLightboxRecents");

        /// <summary>
        /// Gets the recent research list.
        /// </summary>
        /// <value>
        /// The recent research list.
        /// </value>
        public IList<RecentResearchListItem> RecentResearchList => this.GenerateRecentResearchList();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private List<RecentResearchListItem> GenerateRecentResearchList() =>
            DriverExtensions.GetElements(DriverExtensions.GetElement(this.ComponentLocator), By.CssSelector("ul > li"))
                            .Select(element => new RecentResearchListItem(element))
                            .ToList();
    }
}