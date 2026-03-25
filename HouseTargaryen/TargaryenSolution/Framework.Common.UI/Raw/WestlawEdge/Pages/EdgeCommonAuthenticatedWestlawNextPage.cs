namespace Framework.Common.UI.Raw.WestlawEdge.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// IndigoCommonAuthenticatedWestlawNextPage can be used as a base page for IndigoCommonSearchHome and other WestlawNext pages
    /// NOTE: use CommonAuthenticatedWestlawNextPage for pages with the more narrow header and the folder widget
    /// </summary>
    public class EdgeCommonAuthenticatedWestlawNextPage : CommonAuthenticatedWestlawNextPage
    {
        private static readonly By BodyLocator = By.XPath("//body"); 
        private static readonly By BackToTopLocator = By.XPath("//*[@id='co_backToTop']/div/a");

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new EdgeFooterComponent Footer { get; } = new EdgeFooterComponent();

        /// <summary>
        /// Click on page at any point
        /// </summary>
        public void ClickOnBody()
        {
            DriverExtensions.Click(BodyLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// The click see back to top button.
        /// </summary>
        /// <typeparam name="T">
        /// Page type
        /// </typeparam>
        /// <returns>
        /// The desired page
        /// </returns>
        public T ClickBackToTopButton<T>()
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BackToTopLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is back to Top button displayed
        /// </summary>
        /// <returns> true if present </returns>
        public bool IsBackToTopButtonDisplayed() => DriverExtensions.IsDisplayed(BackToTopLocator, 5);
    }
}
