namespace Framework.Common.UI.Products.WestlawEdge.Components.ToC
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base class for Edge ToC components
    /// </summary>
    public abstract class BaseEdgeTocComponent : BaseModuleRegressionComponent
    {
        private const string TocLinkLctMask = ".//div[@class='TocEntryContent']/a/span[contains(text(),'{0}')]";
        private static readonly By TocContainerLocator = By.XPath("//div[@id='co_leftColumn']//div[contains(@id,'ocContainer')]");
        private static readonly By NodTocBrowseIdLocator = By.Id("coid_browseToc");
        private static readonly By TocHeadingExpandLocator = By.XPath(".//*[contains(@class,'TocSectionToggleIcon Icon-collapsed')]");

        /// <summary>
        /// Get ToC links
        /// </summary>
        /// <param name="locator">Locator</param>
        /// <returns></returns>
        public List<string> GetTocLinks(params By[] locator)
        {
            if (DriverExtensions.GetElements(this.ComponentLocator, TocHeadingExpandLocator).Any())
            {
                this.ExpandAllTocEntries();
            }

            return DriverExtensions.GetElements(DriverExtensions.GetElement(this.ComponentLocator), locator)
                                   .Select(elem => elem.Text.Trim()).ToList();
        }

        /// <summary>
        /// The is toc expand displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTocExpandDisplayed() => DriverExtensions.IsDisplayed(TocHeadingExpandLocator);

        /// <summary>
        /// Expand all toc nod entries.
        /// </summary>
        public void ExpandAllTocEntries()
        {
            List<IWebElement> collapsedLinksList = DriverExtensions.GetElements(this.ComponentLocator, TocHeadingExpandLocator).ToList();

            foreach (IWebElement link in collapsedLinksList)
            {
                link.ScrollToElementCenter();
                DriverExtensions.Click(link);
            }
        }

        /// <summary>
        /// Verifies that the TOC section is collapsed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if notes of decisions left rail is displayed </returns>
        public bool IsTocSectionCollapsed() => DriverExtensions.GetElement(NodTocBrowseIdLocator)
                                                                  .GetAttribute("class").Contains("co_hideState");

        /// <summary>
        /// The is toc browse displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTocBrowseDisplayed() => DriverExtensions.IsDisplayed(NodTocBrowseIdLocator);

        /// <summary>
        /// Clicks the left rail heading by name.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="name">
        /// The name of heading. 
        /// </param>
        /// <returns>The new instance of T page/>. 
        /// </returns>
        public T ClickHeadingByName<T>(string name) where T : ICreatablePageObject
        {
            DriverExtensions.ScrollToElementInsideContainer(TocContainerLocator, By.XPath(string.Format(TocLinkLctMask, name)));
            DriverExtensions.GetElement(By.XPath(string.Format(TocLinkLctMask, name))).CustomClick();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the left rail heading by name.
        /// </summary>
        /// <param name="name"> The name of heading. </param>
        /// <returns> The <see cref="EdgeNotesOfDecisionsPage"/>. </returns>
        public EdgeNotesOfDecisionsPage ExpandAllAndClickHeadingByName(string name)
        {
            this.ExpandAllTocEntries();
            DriverExtensions.GetElement(By.XPath(string.Format(TocLinkLctMask, name))).CustomClick();

            return new EdgeNotesOfDecisionsPage();
        }
    }
}