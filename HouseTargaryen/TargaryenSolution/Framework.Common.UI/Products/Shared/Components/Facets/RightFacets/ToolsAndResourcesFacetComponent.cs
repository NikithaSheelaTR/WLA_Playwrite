namespace Framework.Common.UI.Products.Shared.Components.Facets.RightFacets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Tools And Resources Facet Component
    /// Placed on the right side on the some category pages 
    /// </summary>
    public sealed class ToolsAndResourcesFacetComponent : BaseModuleRegressionComponent
    {
        private static readonly By LinkLocator = By.CssSelector("#co_rightColumn .co_genericBox a");

        private static readonly By SummaryTextLocator = By.XPath("//div[@class='co_genericBoxContent']/h4");

        private static readonly By ContainerLocator = By.CssSelector("#co_rightColumn .co_genericBox");

        private EnumPropertyMapper<ToolsLink, WebElementInfo> toolsLinksMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the AwardRange enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<ToolsLink, WebElementInfo> ToolsLinksMap
            => this.toolsLinksMap = this.toolsLinksMap ?? EnumPropertyModelCache.GetMap<ToolsLink, WebElementInfo>();

        /// <summary>
        /// Verify the link is displayed.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsLinkDisplayed(ToolsLink link) => DriverExtensions.IsDisplayed(By.Id(this.ToolsLinksMap[link].Id));

        /// <summary>
        /// Clicks on the Link
        /// </summary>
        /// <param name="link">The link.</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns>
        public T ClickLink<T>(ToolsLink link) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.Id(this.ToolsLinksMap[link].Id)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Link By Text
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="linkText"> Link text </param>
        /// <returns> New instance of the page </returns>
        public override T ClickLinkByText<T>(string linkText)
        {
            IWebElement linkElement = DriverExtensions.GetElements(LinkLocator)
                .FirstOrDefault(link => link.Text.Trim().ToLower().Equals(linkText.Trim().ToLower()));

            if (linkElement != null)
            {
                linkElement.Click();
                return DriverExtensions.CreatePageInstance<T>();
            }

            throw new Exception("Cannot find Link with Text " + linkText);
        }

        /// <summary>
        /// Returns a list of all of the links available in the Tools and Resources
        /// section on the right side of the browse page
        /// </summary>
        /// <returns> A list of strings, where each is the text of one of the links in Tools and Resources </returns>
        public List<string> GetListOfLinks()
            => DriverExtensions.GetElements(LinkLocator).Select(elem => elem.Text.Trim()).ToList();

        /// <summary>
        /// Gets Summary Text inside Tools and Resources widget
        /// </summary>
        /// <returns> Summary Text </returns>
        public string GetSummaryText() => DriverExtensions.GetText(SummaryTextLocator);
    }
}