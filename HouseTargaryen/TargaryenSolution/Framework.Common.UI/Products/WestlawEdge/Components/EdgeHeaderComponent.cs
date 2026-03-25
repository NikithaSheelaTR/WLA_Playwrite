namespace Framework.Common.UI.Products.WestlawEdge.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components.EdgeResponsive;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
	/// Edge Header component
	/// </summary>
	public class EdgeHeaderComponent : WestlawNextHeaderComponent
    {
        private const string SnapshotPersonLctMask = "//div[@id='co_personLink'][{0}]/div/a";

        private static readonly By EdgeTypeAheadLocator = By.Id("coid_trDiscover");
        private static readonly By FactorsCourtsConsiderTypeAheadLocator = By.XPath("//*[@id='contentTypeDetailsContainer']//h3[contains(text(),'Factors Courts Consider')]");
        private static readonly By FullHeaderLocator = By.XPath("//body[contains(@class,'Header-full')]");
        private static readonly By InfoMessageLocator = By.XPath("//div[@class='co_infoBox_inner']/div[@class='co_infoBox_message' and not(./a)]");
        private static readonly By NotificationsBellLocator = By.XPath("//span[contains(@class,'icon_bell')]");
        private static readonly By SnapshotStockTickerLocator = By.XPath("//*[@id='co_personLink']/div[1]");
        private static readonly By SnapShotSectionLocator1 = By.Id("typeAhead_checkboxelastic_snapshots");
        private static readonly By SnapShotSectionLocator2 = By.Id("coid_autoCompleteSection_elastic_snapshots");
        private static readonly By SnapShotSectionLocator3 = By.Id("coid_snapshotExactMatch_elastic_snapshots");
        private static readonly By SnapShotSectionLocator4 = By.Id("co_personLink");
        private static readonly By WestlawLogoLocator = By.XPath("//a[@class='HeaderBrand-link'] | //a[@id='coid_website_logo']");
        private static readonly By ExitFullScreenButtonLocator = By.XPath("//div[@id = 'co_readingModeExitButtonPlaceholder']");
        private static readonly By FullScreenButtonLocator = By.Id("co_fullscreenModeLink");
        private static readonly By DisplayOptionsButtonLocator = By.XPath("//*[@id = 'co_displayOptionsLink']");
        private static readonly By SignOutButtonLocator = By.XPath("//*[contains(@class, 'js_website_signoff')] | //li[@id = 'co_oneClickSignoutContainer']");        
        private static readonly By ExpandedTabLocator = By.XPath(".//ancestor::div[@class='co_dropdownTabExpanded']");
        private static readonly By HamburgerMenuButtonLocator = By.XPath("//span[contains(@class, 'hamburgerMenu')]//ancestor::div[contains(@class, 'co_menuToggleButtonContainer')]");
        private static readonly By ExpandHeaderLocator = By.XPath("//div[@id ='co_headerToggleContainer']");
        private static readonly By HeaderWrapperForBackgroundColorLocator = By.Id("co_headerWrapper");
        private static readonly By ToolsFlyoutButtonLocator = By.XPath("//*[@id='co_mainNav']//*[contains(@class, 'Tools-flyout-AccessPoint')]//button[@oldtitle='Tools']");

        private EnumPropertyMapper<EdgeHeaderTabs, WebElementInfo> edgeHeaderTabsMap;

        /// <summary>
        /// Region Dropdown 
        /// </summary>
        public EdgeRegionDropdown EdgeRegionDropdown { get; } = new EdgeRegionDropdown();

        /// <summary>
        /// Full Screen Button
        /// </summary>
        public IButton FullScreenButton => new Button(FullScreenButtonLocator);

        /// <summary>
        /// Expand header button
        /// </summary>
        public IButton ExpandHeaderButton => new Button(ExpandHeaderLocator);

        /// <summary>
        /// Expand Tools Flyout button
        /// </summary>
        public IButton ExpandToolsFlyoutButton => new Button(ToolsFlyoutButtonLocator);

        /// <summary>
        /// The Hamburger menu button
        /// </summary>
        public IButton HamburgerMenuButton => new Button(HamburgerMenuButtonLocator);

        /// <summary>
        /// Gets the Edge Header tabs enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<EdgeHeaderTabs, WebElementInfo> EdgeHeaderTabsMap =>
            this.edgeHeaderTabsMap = this.edgeHeaderTabsMap
                                     ?? EnumPropertyModelCache.GetMap<EdgeHeaderTabs, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdge/Header");

        /// <summary>
        /// Clicks Westlaw logo in Indigo. 
        /// Note: the method hides the base one, since the behavior of the header has changed: when a user scrolls down, the header gets "frozen", but it gest smaller. However, under the hood,
        /// there are in fact two headers, hence two logos, namely: the primary and the secondary one, yet only one is displayed at a time, e.g.:
        /// IndigoHomePage - the primary one is displayed [XPath: //div[@class='HeaderBrand HeaderBrand--primary']//a[@class='HeaderBrand-link']
        /// IndigoCommonSearchResultPage - the secondary one is displayed, when a user scrolls down [XPath: //div[@class='HeaderBrand HeaderBrand--secondary']//a[@class='HeaderBrand-link']
        /// Since there is only one IndigoWestlawHeader object, hence the "tricky" method below.
        /// </summary>
        /// <typeparam name="T">Page to instantiat</typeparam>
        /// <returns>The instantiated page</returns>
        public new T ClickLogo<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(WestlawLogoLocator).First(x => x.Displayed).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Hamburger menu button
        /// </summary>
        /// <returns>New instance of GlobalNavigationRightPaneComponent.</returns>
        public GlobalNavigationRightPaneComponent ClickHamburgerMenuButton()
        {
            HamburgerMenuButton.Click();
            return new GlobalNavigationRightPaneComponent();
        }

        /// <summary>
        /// Clicks Westlaw Edge header tab
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the button</typeparam>
        /// <param name="tab">Indigo header tab</param>
        /// <returns>New instance of T</returns>
        public T ClickHeaderTab<T>(EdgeHeaderTabs tab) where T : ICreatablePageObject
        {
            if (!this.IsHeaderTabExpanded(tab))
            {
                DriverExtensions.Click(DriverExtensions.WaitForElement(By.XPath(this.EdgeHeaderTabsMap[tab].LocatorString)));
            }
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify is Westlaw Edge header tab displayed in the page
        /// </summary>
        /// <param name="tab">tab to verify</param>
        /// <returns>trye if displayed</returns>
        public bool IsHeaderTabDisplayed(EdgeHeaderTabs tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.EdgeHeaderTabsMap[tab].LocatorString));

        /// <summary>
        /// Verify is Westlaw Edge header tab is expanded
        /// </summary>
        /// <param name="tab">tab to verify</param>
        /// <returns>true if displayed</returns>
        public bool IsHeaderTabExpanded(EdgeHeaderTabs tab) =>
            DriverExtensions.IsDisplayed(By.XPath(this.EdgeHeaderTabsMap[tab].LocatorString), ExpandedTabLocator);

        /// <summary>
        /// The click snap shot person link.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> The <see cref="ExpertProfilePage"/>. </returns>
        public EdgeExpertProfilePage ClickSnapshotPersonLink(int index)
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(SnapshotPersonLctMask, index)).Click();
            return new EdgeExpertProfilePage();
        }

        /// <summary>
        /// Get Client ID text
        /// </summary>
        /// <returns>The client Id text</returns>
        public new string GetClientIdText() => DriverExtensions
                                               .WaitForElement(
                                                   By.XPath(
                                                       this.EdgeHeaderTabsMap[EdgeHeaderTabs.ClientId].LocatorString))
                                               .Text;

        /// <summary>
        /// Returns the common confirmation/ failure message displayed for any action performed
        /// on Search results Page, History page, Folder page, Document page 
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public virtual string GetInfoMessage()
        {
            DriverExtensions.WaitForElement(InfoMessageLocator);
            IWebElement messageElement = DriverExtensions.GetElements(InfoMessageLocator).FirstOrDefault(el => el.Displayed);
            return messageElement.GetText();
        }

        /// <summary>
        /// Returns the common confirmation/ failure message displayed for any action performed
        /// on Search results Page, History page, Folder page, Document page 
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public bool IsInfoMessageAppears(string message)
        {
            DriverExtensions.WaitForElement(InfoMessageLocator);
            return DriverExtensions.GetElements(InfoMessageLocator).Any(el => el.Text.Contains(message));
        }

        /// <summary>
        /// Get list of recent folder
        /// </summary>
        /// <returns>
        /// The list of recent folders.
        /// </returns>
        public new List<string> GetRecentFolders()
        {
            var recentFolderDialog = this.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders);
            List<string> recentFolders = recentFolderDialog.GetFoldersNames();
            recentFolderDialog.CloseButton.Click();

            return recentFolders;
        }

        /// <summary>
        /// Verification if Indigo Typeahead dialog is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsEdgeTypeAheadDisplayed() => DriverExtensions.IsDisplayed(EdgeTypeAheadLocator, 5);

        /// <summary>
        /// IsFactorsCourtsConsiderDisplayed
        /// </summary>
        /// <returns>True if Factors Courts Consider is displayed, otherwise - false</returns>
        public bool IsFactorsCourtsConsiderDisplayed() => DriverExtensions.IsDisplayed(FactorsCourtsConsiderTypeAheadLocator, 5);

        /// <summary>
        /// Is full header displayed
        /// </summary>
        /// <returns></returns>
        public bool IsFullHeaderDisplayed() => DriverExtensions.IsDisplayed(FullHeaderLocator, 5);

        /// <summary>
        /// The is notifications bell icon displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsNotificationsBellIconDisplayed() => DriverExtensions.IsDisplayed(NotificationsBellLocator);

        /// <summary>
        /// Returns true if stock ticker is displayed
        /// </summary>
        /// <returns>True if displayed</returns>
        public bool IsPersonSnapShotOnTypeAhead() => DriverExtensions.IsDisplayed(SnapshotStockTickerLocator, 5);

        /// <summary>
        /// Returns true if snapshot section is displayed else false
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSnapShotDisplayed() => DriverExtensions.IsDisplayed(SnapShotSectionLocator1, 2)
                                             || DriverExtensions.IsDisplayed(SnapShotSectionLocator2, 2)
                                             || DriverExtensions.IsDisplayed(SnapShotSectionLocator3, 2)
                                             || DriverExtensions.IsDisplayed(SnapShotSectionLocator4, 2);

        /// <summary>
        /// Is Exit full screen button displayed
        /// </summary>
        /// <returns></returns>
        public bool IsExitFullScreenButtonDisplayed() =>
            DriverExtensions.IsDisplayed(ExitFullScreenButtonLocator, 5);

        /// <summary>
        /// Click on the Exit full screen button.
        /// </summary>
        /// <returns>
        /// The <see cref="EdgeCommonDocumentPage"/>.</returns>
        public EdgeCommonDocumentPage ClickExitFullScreenButton()
        {
            DriverExtensions.WaitForElement(ExitFullScreenButtonLocator).Click();
            return new EdgeCommonDocumentPage();
        }
        /// <summary>
        /// Click on Display Options link
        /// </summary>
        /// <returns>The <see cref="EdgeDisplayOptionsDialog"/>.</returns>
        public EdgeDisplayOptionsDialog OpenDisplayOptionsDialog()
        {
            DriverExtensions.WaitForElement(DisplayOptionsButtonLocator).Click();
            return new EdgeDisplayOptionsDialog();
        }

        /// <summary>
        /// Returns true if Display Options button is displayed
        /// </summary>
        /// <returns>True if displayed</returns>
        public bool IsDisplayOptionsButtonDisplayed() =>
            DriverExtensions.IsDisplayed(DisplayOptionsButtonLocator, 5);

        /// <summary>
        /// Click on Sign out button
        /// </summary>
        /// <returns>The <see cref="CommonSignOffPage"/>.</returns>
        public CommonSignOffPage ClickSignOutButton()
        {
            DriverExtensions.Click(SignOutButtonLocator);
            return new CommonSignOffPage();
        }

        /// <summary>
        /// Get BackGround Color
        /// </summary>
        public string GetBackGroundColor() => DriverExtensions.GetElement(HeaderWrapperForBackgroundColorLocator).GetCssValue("color");
    }
}