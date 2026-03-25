namespace Framework.Common.UI.Products.GovernmentWeblinks.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.GovernmentWeblinks.Models;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// WeblinksResultListComponent
    /// </summary>
    public class WeblinksResultListComponent : BaseModuleRegressionComponent
    {
        private static readonly By IndexLocator = By.XPath(".//*[@class='co_resultsListCount']");

        private static readonly By TitleLocator = By.XPath(".//a[@class='resultLink']");

        private static readonly By DescriptionLocator = By.XPath(".//*[@class='co_resultsListDescription']");

        private static readonly By ContainerLocator = By.Id("results");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets total results
        /// </summary>
        public int Count => this.Items.Count;

        /// <summary>
        /// Gets the result list.
        /// </summary>
        public List<WeblinksSearchResultModel> Items => DriverExtensions
            .GetElements(this.ComponentLocator, By.TagName("li")).Select(
                e => new WeblinksSearchResultModel(
                    int.Parse(DriverExtensions.WaitForElement(e, IndexLocator).Text.Replace(".", string.Empty)),
                    DriverExtensions.WaitForElement(e, TitleLocator).Text,
                    DriverExtensions.WaitForElement(e, DescriptionLocator).Text,
                    DriverExtensions.WaitForElement(e, TitleLocator).GetAttribute("href"))).ToList();
    }
}
