namespace Framework.Common.UI.Products.GovernmentWeblinks.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.GovernmentWeblinks.Enums;
    using Framework.Common.UI.Products.GovernmentWeblinks.Pages;
    using Framework.Common.UI.Products.GovernmentWeblinks.Pages.Standard;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Toc component for weblinks product
    /// </summary>
    public class WeblinksTocComponent : WeblinksHeaderComponent
    {
        private static readonly By DocLinkLocator = By.XPath("//ul[@class='co_genericWhiteBox']/li/a[contains(@href, 'Document')]");

        private static readonly By TocComponentLocator = By.ClassName("co_genericWhiteBox");

        private static readonly By TocLinkLocator = By.XPath("//ul[@class='co_genericWhiteBox']/li/a[contains(@href, 'Browse')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TocComponentLocator;

        /// <summary>
        /// Clicks on Toc title
        /// </summary>
        /// <param name="index">The index of toc title. Starts from zero</param>
        /// <returns>The instance of the page</returns>
        public WeblinksDocumentPage ClickDocumentByIndex(int index)
        {
            DriverExtensions.GetElements(DocLinkLocator).ElementAt(index).Click();
            return new WeblinksDocumentPage();
        }

        /// <summary>
        /// Clicks on Toc title
        /// </summary>
        /// <param name="index">The index of toc title. Starts from zero</param>
        /// <returns>The instance of the page</returns>
        public StandardTocPage ClickTocByIndex(int index)
        {
            DriverExtensions.GetElements(TocLinkLocator).Where(e => !ConstantsManager.WebLinks[WebLinks.OutOfSubscriptionLinks].Links.Contains(e.Text)).ElementAt(index).Click();
            return new StandardTocPage();
        }

        /// <summary>
        /// Gets the document list.
        /// </summary>
        /// <returns>The list of titles</returns>
        public List<string> GetDocumentList() => DriverExtensions.GetElements(DocLinkLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Gets the toc list.
        /// </summary>
        /// <returns>The list of titles</returns>
        public List<string> GetTocList() => DriverExtensions.GetElements(TocLinkLocator).Select(e => e.Text).Except(ConstantsManager.WebLinks[WebLinks.OutOfSubscriptionLinks].Links).ToList();

        /// <summary>
        /// Verifies is component displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(TocComponentLocator, 5);
    }
}
