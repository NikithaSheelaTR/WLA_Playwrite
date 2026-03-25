namespace Framework.Common.UI.Raw.WestlawEdge.Pages.LegalAnalaytics
{
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Analytics View Page
    /// </summary>
    public class AnalyticsViewPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By ViewMotionAnalyticsButtonLocator = By.XPath("//button[text() = 'View Motion Analytics']");
        private static readonly By LaViewPageLocator = By.TagName("la-trd-component");
        private static readonly By TitleProfileLocator = By.XPath("//la-nlg-profile-widget//h2");

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string ProfileTitle => DriverExtensions.GetText(TitleProfileLocator);

        /// <summary>
        /// Graph component
        /// </summary>
        public LegalAnalyticsTabPanel LagalAnalyticsTabPanel { get; } = new LegalAnalyticsTabPanel();
       
        /// <summary>
        /// Click on View Motion Analytics button
        /// </summary>
        /// <returns></returns>
        public AnalyticsProfilerPage ClickAnalyticsButton() 
        {
            DriverExtensions.WaitForElement(ViewMotionAnalyticsButtonLocator,60000).Click();
            return new AnalyticsProfilerPage();
        }

        /// <summary>
        /// The is page displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsPageDisplayed()
        {
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.IsDisplayed(LaViewPageLocator, 5);
        }
    }
}
