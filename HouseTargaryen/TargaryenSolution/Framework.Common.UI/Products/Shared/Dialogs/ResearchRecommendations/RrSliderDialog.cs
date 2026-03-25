namespace Framework.Common.UI.Products.Shared.Dialogs.ResearchRecommendations
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.ResearchRecommendations;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// This class contains elements and methods pertaining to a RA slider modal page
    /// </summary>
    public class RrSliderDialog : BaseModuleRegressionDialog
    {
        private const string NewestRecommendationsText = "View newest recommendations";

        private const string PreviousRecommendationsText = "View previous recommendations";

        private const string TurnOffLinkText = "Turn off for this session";

        private const string TurnOnLinkText = "Turn on";

        private static readonly By ResearchRecommendationsInfoLocator = By.Id("coid_raInfo");

        private static readonly By CloseIconLocator = By.XPath("//div[@id='co_ra_closeIcon']/button");

        private static readonly By FullScreenModeButtonLocator = By.CssSelector("#co_ra_moreResultsLink");

        private static readonly By DateAndClientIdLocator = By.Id("coid_ra_dateTimeContainer");

        private static readonly By NoResultsLocator = By.XPath("//div[@class='co_noResultsSection']");

        private static readonly By HeaderLocator = By.XPath("//div[contains(@class,'show')]//h1[@id='coid_contentSliderTitle'][text()='Research Recommendations']");

        private static readonly By PrevNewestRecommsOffLocator = By.Id("coid_historyRecommendation");

        private static readonly By TurnOnOffButtonLocator = By.Id("coid_turnOffLink");

        private static readonly By InfoMessageLocator = By.XPath("//*[contains(@id,'coid_a11yTooltip_44')]//div[contains(text(), 'Research Recommendations')]");

        /// <summary>
        /// Constructor for the ResearchAcceleratorModal object
        /// </summary>
        public RrSliderDialog()
        {
            DriverExtensions.WaitForElementDisplayed(HeaderLocator);
        }

        /// <summary>
        /// Key Number Section
        /// </summary>
        public KeyNumbersComponent KeyNumbersComponent { get; protected set; } = new KeyNumbersComponent();
        
        /// <summary>
        /// Research Recommendations Result List
        /// </summary>
        public RrTabPanelComponent Tabs { get; protected set; } = new RrTabPanelComponent();

        /// <summary>
        /// Why am I see/getting these recommendations Component
        /// </summary>
        public WaistrComponent WaistrComponent { get; protected set; } = new WaistrComponent();

        /// <summary>
        /// Click RR Info button "i"
        /// </summary>
        public void ClickRrInfo() => DriverExtensions.WaitForElementDisplayed(ResearchRecommendationsInfoLocator).Hover();

        /// <summary>
        /// This method clicks close Button
        /// </summary>
        /// <typeparam name="T">ICreatable PageObject</typeparam>
        /// <returns>PageObject</returns>
        public T ClickClose<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseIconLocator);

        /// <summary>
        /// Click full view button. 
        /// </summary>
        /// <typeparam name="T">ICreatable PageObject</typeparam>
        /// <returns>PageObject</returns>
        public T ClickFullViewMode<T>() where T : ICreatablePageObject => this.ClickElement<T>(FullScreenModeButtonLocator);

        /// <summary>
        /// Click RA header, e.g. to dismiss pop-up or move focus
        /// </summary>
        public void ClickResearchRecommendationHeader() => this.ClickElement(HeaderLocator);

        /// <summary>
        /// Click 'Turn off for this session' link
        /// </summary>
        /// <returns>new instance of TurnOffRaDialog</returns>
        public TurnOffRrDialog ClickTurnOff() => this.ClickElement<TurnOffRrDialog>(TurnOnOffButtonLocator);

        /// <summary>
        /// Click 'Turn On' link
        /// </summary>
        public void ClickTurnOn() => this.ClickElement(TurnOnOffButtonLocator);

        /// <summary>
        /// Click link View Previous/Newest Recommendations
        /// </summary>
        /// <returns>new instance of RrSliderDialog</returns>
        public RrSliderDialog ClickViewPrevNewestRecommendations() => this.ClickElement<RrSliderDialog>(PrevNewestRecommsOffLocator);

        /// <summary>
        /// Gets date and specified client Id
        /// </summary>
        /// <returns>The date and the client ID</returns>
        public string[] GetDateAndClientId() =>
            DriverExtensions.WaitForElementDisplayed(DateAndClientIdLocator).Text.Split(new[] { " | " }, StringSplitOptions.None);

        /// <summary>
        /// GetInfo Icon Text
        /// GetInfo Icon Text
        /// </summary>
        /// <returns> text </returns>
        public string GetInfoIconText() => DriverExtensions.GetText(InfoMessageLocator);

        /// <summary>
        /// Verify Full screen widget present
        /// </summary>
        /// <returns>true if Full screen widget present, false otherwise</returns>
        public bool IsFullScreenButtonDisplayed() => DriverExtensions.IsDisplayed(FullScreenModeButtonLocator, 5);

        /// <summary>
        /// Verify RA Info button "i" displayed
        /// </summary>
        /// <returns>true if RA Info button "i" displayed, false otherwise</returns>
        public bool IsInfoButtonDisplayed() => DriverExtensions.IsDisplayed(ResearchRecommendationsInfoLocator, 5);

        /// <summary>
        /// Verify CloseIconLocator button is displayed
        /// </summary>
        /// <returns>true if CloseIconLocator button is displayed, false otherwise</returns>
        public bool IsCloseButtonDisplayed() => DriverExtensions.IsDisplayed(CloseIconLocator, 5);

        /// <summary>
        /// Verify valid date and specified client ID are displayed (in the bottom of the Slider)
        /// </summary>
        /// <returns>true if valid date and specified client ID are displayed, false otherwise</returns>
        public bool IsDateAndClientIdDisplayed() => DriverExtensions.IsDisplayed(DateAndClientIdLocator, 5);

        /// <summary>
        /// Verify 'View Newest Recommendations' link appeared
        /// </summary>
        /// <returns>true if 'View Newest Recommendations' link appeared, false otherwise</returns>
        public bool IsViewNewestRecommendationsDisplayed() =>
            DriverExtensions.IsDisplayed(PrevNewestRecommsOffLocator, 5) && DriverExtensions.WaitForElementDisplayed(PrevNewestRecommsOffLocator).Text == NewestRecommendationsText;

        /// <summary>
        /// Verify 'View Previous Recommendations' link appeared
        /// </summary>
        /// <returns>true if 'View Previous Recommendations' link appeared, false otherwise</returns>
        public bool IsViewPrevRecommendationsDisplayed() =>
            DriverExtensions.IsDisplayed(PrevNewestRecommsOffLocator, 5) && DriverExtensions.WaitForElementDisplayed(PrevNewestRecommsOffLocator).Text == PreviousRecommendationsText;

        /// <summary>
        /// Verify Slider displays the specified no results message
        /// </summary>
        /// <param name="noResultsMsg">no results message text</param>
        /// <returns>true if specified no results message is displayed, false otherwise</returns>
        public bool IsNoResultsMessageDisplayed(string noResultsMsg) =>
            DriverExtensions.IsDisplayed(NoResultsLocator, 5) && DriverExtensions.GetElement(NoResultsLocator).Text.Contains(noResultsMsg);

        /// <summary>
        /// Verify slider with 'Research Recommendations' label is displayed
        /// </summary>
        /// <returns>true if Research Recommendations is opened, false otherwise</returns>
        public bool IsRrSliderDisplayed() => DriverExtensions.IsDisplayed(HeaderLocator, 5);

        /// <summary>
        /// Verify "Turn off for this session" link present
        /// </summary>
        /// <returns>true if "Turn off for this session" link present, false otherwise</returns>
        public bool IsTurnOffLinkDisplayed() =>
            DriverExtensions.IsDisplayed(TurnOnOffButtonLocator, 5) && DriverExtensions.WaitForElementDisplayed(TurnOnOffButtonLocator).Text == TurnOffLinkText;

        /// <summary>
        /// Verify "Turn On" link present
        /// </summary>
        /// <returns>true if "Turn On" link present, false otherwise</returns>
        public bool IsTurnOnLinkDisplayed() =>
            DriverExtensions.IsDisplayed(TurnOnOffButtonLocator, 5) && DriverExtensions.WaitForElementDisplayed(TurnOnOffButtonLocator).Text == TurnOnLinkText;
    }
}