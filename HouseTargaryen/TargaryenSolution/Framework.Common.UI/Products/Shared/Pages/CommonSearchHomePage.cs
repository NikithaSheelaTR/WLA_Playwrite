namespace Framework.Common.UI.Products.Shared.Pages
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.HomePage;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// CommonSearchHomePage - initial landing page for WestlawNext users
    /// </summary>
    public class CommonSearchHomePage : CommonAuthenticatedWestlawNextPage, ICommonSearchHomePage
    {
        private const string TabNameOfPage = "Westlaw";

        private static readonly By DynamicMessageLocator = By.Id("co_dynamicMessageContainer");

        private static readonly By EditHomePageLinkLocator = By.Id("coid_website_homePersonalizationLink");

        private static readonly By FolderWidgetLocator = By.Id("co_dockTitle");

        private static readonly By FrequentlyUsedWidgetLocator = By.Id("coid_website_frequentlyUsedWidget");
        
        private static readonly By InfoBoxErrorMessageBoxLocator = By.XPath("//div[@id = 'co_subHeader']/div[@class = 'co_infoBox failure']");

        private static readonly By InfoBoxMessageLocator = By.ClassName("co_infoBox_message");

        private static readonly By InfoBoxWarningMessageBoxLocator = By.CssSelector(".co_infoBox.warning");

        private static readonly By LiveChatLinkLocator = By.Id("coid_websiteFooter_livechatlink");

        private static readonly By NewsWidgetLocator = By.XPath("//*[text() = 'News and Insight from Reuters']");

        private static readonly By SaoMessageLocator = By.XPath("//div[@class='co_infoBox success']//div[@class='co_infoBox_message']");

        private static readonly By HomeBodyLocator = By.CssSelector("body.co_homepage");

        /// <summary>
        /// Browse Component
        /// </summary>
        public BrowseTabPanel BrowseTabPanel { get; } = new BrowseTabPanel();

        /// <summary>
        /// Custom Pages Widget on the right hand side
        /// </summary>
        public CustomPagesComponent CustomPagesWidget { get; } = new CustomPagesComponent();

        /// <summary>
        /// Favorites Widget on the right hand side
        /// </summary>
        public FavoritesComponent Favorites { get; } = new FavoritesComponent();

        /// <summary>
        /// Edge promotion banner on the Home page of Westlaw Classic
        /// </summary>
        public EdgePromotionBannerComponent PromotionBanner { get; } = new EdgePromotionBannerComponent();

        /// <summary>
        /// Clicks the edit homepage link
        /// </summary>
        /// <returns>The dialog for editing homepage link</returns>
        public CustomizeHomepageDialog ClickEditHomepageLink()
        {
            DriverExtensions.WaitForElement(EditHomePageLinkLocator).Click();
            return new CustomizeHomepageDialog();
        }

        /// <summary>
        /// Checks if the folder widget is displayed
        /// </summary>
        /// <returns>If the folder widget exists</returns>
        public bool IsFolderWidgetDisplayed() => DriverExtensions.IsDisplayed(FolderWidgetLocator);

        /// <summary>
        /// Gets the text of the failure info box header
        /// </summary>
        /// <returns>The header text</returns>
        public string GetFailureInfoBoxHeader() => DriverExtensions.GetImmediateText(InfoBoxErrorMessageBoxLocator, By.TagName("h4")).Trim();

        /// <summary>
        /// Gets the text of the failure info box
        /// </summary>
        /// <returns>The message text</returns>
        public string GetFailureInfoBoxMessage() => DriverExtensions.GetImmediateText(InfoBoxErrorMessageBoxLocator, InfoBoxMessageLocator).Trim();

        /// <summary>
        /// Gets the confirmation message for acceptance and declines for SAO on the Search Home Page
        /// </summary>
        /// <returns>confirmation message</returns>
        public string GetSaoConfirmationMessage() => DriverExtensions.WaitForElement(SaoMessageLocator).GetText();

        /// <summary>
        /// Gets the text of the warning info box header
        /// </summary>
        /// <returns>The header text</returns>
        public string GetWarningInfoBoxHeader() => DriverExtensions.GetImmediateText(InfoBoxWarningMessageBoxLocator, By.TagName("h4")).Trim();

        /// <summary>
        /// Gets the text of the warning info box
        /// </summary>
        /// <returns>The message text</returns>
        public string GetWarningInfoBoxMessage() => DriverExtensions.GetImmediateText(InfoBoxWarningMessageBoxLocator, InfoBoxMessageLocator).Trim();

        /// <summary>
        /// Checks whether or not the user has navigated to the Westlaw home page
        /// </summary>
        /// <returns></returns>
        public bool IsHomePageDisplayed()
            => BrowserPool.CurrentBrowser.Title.Equals(TabNameOfPage, StringComparison.InvariantCultureIgnoreCase)
                && DriverExtensions.IsDisplayed(HomeBodyLocator);

        /// <summary>
        /// Determines if the Dynamic Messaging promo is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDynamicMessagingPromoDisplayed() => DriverExtensions.IsDisplayed(DynamicMessageLocator);

        /// <summary>
        /// Determines if the failure info box displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsFailureInfoBoxDisplayed() => DriverExtensions.GetElements(InfoBoxErrorMessageBoxLocator).FirstOrDefault() != null;

        /// <summary>
        /// Checks to see Edit Home Page link is displayed on the home page
        /// </summary>
        /// <returns>true if the link is displayed</returns>
        public bool IsEditHomePageDisplayed() => DriverExtensions.IsDisplayed(EditHomePageLinkLocator);

        /// <summary>
        /// Checks to see if frequently used items widget is displayed on the home page
        /// </summary>
        /// <returns>true if the widget is displayed</returns>
        public bool IsFrequentlyUsedWidgetDisplayed() => DriverExtensions.IsDisplayed(FrequentlyUsedWidgetLocator);

        /// <summary>
        /// Determines if the Live Chat link is present on the homepage
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsLiveChatLinkDisplayed() => DriverExtensions.IsDisplayed(LiveChatLinkLocator);

        /// <summary>
        /// Checks whether the max limit warning is displayed or not.
        /// </summary>
        /// <returns>Boolean</returns>
        public bool IsMaxLimitWarningDisplay()
            => DriverExtensions.IsTextOnPage("Delivery requests allowed for this password have been exceeded")
                && DriverExtensions.IsTextOnPage("Delivery Disabled");

        /// <summary>
        /// Determines if the warning info box displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsWarningInfoBoxDisplayed() => DriverExtensions.IsDisplayed(InfoBoxWarningMessageBoxLocator);

        /// <summary>
        /// Checks if the News section exists
        /// </summary>
        /// <returns>If the favorites section exists</returns>
        public bool IsNewsSectionDisplayed() => DriverExtensions.IsDisplayed(NewsWidgetLocator);
    }
}