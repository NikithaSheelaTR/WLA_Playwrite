namespace Framework.Common.UI.Products.WestLawNext.Pages.IphoneMode
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Components.IphoneMode;
    using Framework.Common.UI.Products.WestLawNext.Enums.IphoneMode;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Landing page class
    /// </summary>
    public class LandingPage : BaseModuleRegressionPage
    {
        private EnumPropertyMapper<LandingPageNavigationOptions, BaseTextModel> landingPageNavigationMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="LandingPage"/> class. 
        /// Constructor for the Landing page
        /// </summary>
        public LandingPage()
        {
            DriverExtensions.WaitForElement(By.LinkText(LandingPageNavigationOptions.Folders.GetStringValue()));
        }

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public MobileSearchHeader Header { get; set; } = new MobileSearchHeader();

        /// <summary>
        /// Preference mode map
        /// </summary>
        protected EnumPropertyMapper<LandingPageNavigationOptions, BaseTextModel> LandingPageNavigationMap
            =>
                this.landingPageNavigationMap =
                    this.landingPageNavigationMap ?? EnumPropertyModelCache.GetMap<LandingPageNavigationOptions, BaseTextModel>();

        /// <summary>
        /// Navigates to page.
        /// </summary>
        /// <param name="link"> The link. </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T NavigateToPage<T>(LandingPageNavigationOptions link) where T : ICreatablePageObject
        {
            By linkLocator = By.LinkText(this.LandingPageNavigationMap[link].Text);

            if (!DriverExtensions.IsDisplayed(linkLocator, 5))
            {
                throw new NoSuchElementException($"Link: '{this.LandingPageNavigationMap[link].Text}' Not Found!");
            }

            DriverExtensions.WaitForElementDisplayed(linkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}