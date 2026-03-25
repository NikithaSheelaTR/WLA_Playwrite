namespace Framework.Common.UI.Products.WestLawNextCanada.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.HomePage;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Miscellaneous;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The common search canada home page.
    /// </summary>
    public class CanadaEdgeHomePage : CanadaEdgeCommonAuthenticatedWestlawNextPage, ICommonSearchHomePage
    {
        private static readonly By PreferenceDialogLocator = By.XPath("//div[@class = 'co_overlayBox_container Preferences-modal']");
        private static readonly By ContentTabSettingsBtnLocator = By.XPath("//span[@id='coid_tabPreference']/button");

        /// <summary>
        /// Gets the Miscellaneous component
        /// </summary>
        public CanadaMiscellaneousComponent MiscellaneousComponent { get; } = new CanadaMiscellaneousComponent();

        /// <summary>
        /// Get the Browse Component
        /// </summary>
        public CanadaBrowseTabPanel BrowseTabPanel { get; } = new CanadaBrowseTabPanel();

        /// <summary>
        /// Get the Key Features Component
        /// </summary>
        public KeyFeaturesComponent KeyFeaturesComponent { get; } = new KeyFeaturesComponent();

        /// <summary>
        /// Canada Edge Footer Component
        /// </summary>
        public CanadaNextFooterComponent CanadaFooter { get; } = new CanadaNextFooterComponent();

        /// <summary>
        /// Waits for preference Dialog to close
        /// </summary>
        public T WaitForPreferenceDialogToClose<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForCondition(condition => !DriverExtensions.IsDisplayed(PreferenceDialogLocator));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on Content Tab Settings Button
        /// </summary>
        /// <typeparam name="T">Page object</typeparam>
        /// <returns>Page instance</returns>
        public T ClickContentTabSettingsButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ContentTabSettingsBtnLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}