namespace Framework.Common.UI.Products.WestLawNext.Pages.SecondarySources.PublicationLandingPages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The publication landing page.
    /// </summary>
    public class PublicationLandingPage : CommonBrowsePage
    {
        private const string DocumentLinkLctMask = "//a[contains(text(),'{0}')]";

        private static readonly By LeftColumnLocator = By.Id("co_leftColumn");

        private static readonly By ScopeIconLocator = By.Id("coid_website_browsePageScopeMoreInfo");

        /// <summary>
        /// Scope Icon Button
        /// </summary>
        public IButton ScopeIcon => new Button(ScopeIconLocator);

        /// <summary>
        /// The display publication document.
        /// </summary>
        /// <param name="docTitle">
        /// The doc title.
        /// </param>
        /// <typeparam name="T">
        /// ICreatablePageObject
        /// </typeparam>
        /// <returns>Instance of an object</returns>
        public T ClickDocumentLink<T>(string docTitle) where T : ICreatablePageObject
        {
            IWebElement documentLink =
                DriverExtensions.WaitForElement(By.XPath(string.Format(DocumentLinkLctMask, docTitle)));
            documentLink.ScrollToElement();
            documentLink.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The is left column displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsLeftColumnDisplayed() => DriverExtensions.IsDisplayed(LeftColumnLocator, 5);
    }
}