namespace Framework.Common.UI.Products.WestLawNext.Pages.PortalManagerSearch.HtmlFilePages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Content Link Portal
    /// </summary>
    public class ContentLinkPortalPage : BaseModuleRegressionPage
    {
        private const string LinkLctMask = "//form[@id='aspnetForm']//a[contains(text(),'{0}')]";

        private static readonly By AllLinksLocator = By.XPath("//form[@id='aspnetForm']//a");

        private static readonly By FormLocator = By.Id("aspnetForm");

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLinkPortalPage"/> class. 
        /// Constructs ContentLinkPortal
        /// </summary>
        public ContentLinkPortalPage()
        {
            DriverExtensions.WaitForElement(FormLocator);
        }

        /// <summary>
        /// Returns a list of links
        /// </summary>
        /// <returns>The list of link's text</returns>
        public List<string> GetLinksText() => DriverExtensions.GetElements(AllLinksLocator).Select(link => link.Text).ToList();

        /// <summary>
        /// Click on the specified link
        /// </summary>
        /// <param name="linkText">The Link text.</param>
        /// <typeparam name="T">Page object instance</typeparam>
        /// <returns>The page object.</returns>
        public T ClickLink<T>(string linkText) where T : BaseModuleRegressionPage
        {
            DriverExtensions.GetElement(By.XPath(string.Format(LinkLctMask, linkText))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}