namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The table of contents component.
    /// </summary>                                                              
    public class TableOfContentsComponent : BaseModuleRegressionComponent
    {
        private static readonly By SuperBrowseIconLinkLocator = By.CssSelector("a.icon_superbrowse");

        private static readonly By TocLinkLocator = By.CssSelector("a.co_prelimNodeLink");

        private static readonly By ContainerLocator = By.CssSelector(".co_documentHead .co_genericBoxContent>.co_genericBoxContentRight");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click Super Browse Icon By Index
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="numberOfSuperBrowseIcon"> index  </param>
        /// <returns> The <see cref="BaseSuperBrowsePage"/>.  </returns>
        public T ClickSuperBrowseIconByIndex<T>(int numberOfSuperBrowseIcon) where T : BaseSuperBrowsePage
        {
            IList<IWebElement> superBrowseIconLinks = this.GetSuperBrowseIconLinks();

            if (superBrowseIconLinks.Count < numberOfSuperBrowseIcon)
            {
                throw new Exception("No Super browse link with index " + numberOfSuperBrowseIcon + " on the page");
            }

            superBrowseIconLinks.ToList()[numberOfSuperBrowseIcon].Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the last link in table of contents component
        /// </summary>
        /// <returns> The <see cref="TableOfContentsBrowsePage"/>. </returns>
        public TableOfContentsBrowsePage ClickTocLastLink()
        {
            this.GetTocLinks().Last().Click();
            return new TableOfContentsBrowsePage();
        }

        /// <summary>
        /// Get text of all links in table of contents component
        /// </summary>
        /// <returns> The <see cref="TableOfContentsBrowsePage"/>. </returns>
        public IList<string> GetTextOfTocLinks()
        {
            return this.GetTocLinks().Select(x => x.Text).ToList();
        }

        /// <summary>
        /// Get count of Super Browse icons
        /// </summary>
        /// <returns> Count of Super Browse icons</returns>
        public int GetCountOfSuperBrowseIcons() => this.GetSuperBrowseIconLinks().Count;

        /// <summary>
        /// Get Super Browse icon links
        /// </summary>
        /// <returns> List of Super Browse icon links </returns>
        private IList<IWebElement> GetSuperBrowseIconLinks() => DriverExtensions.GetElements(this.ComponentLocator, SuperBrowseIconLinkLocator);

        /// <summary>
        /// Get Table of content links
        /// </summary>
        /// <returns> List of Table of content links </returns>
        private IList<IWebElement> GetTocLinks() => DriverExtensions.GetElements(this.ComponentLocator, TocLinkLocator);
    }
}