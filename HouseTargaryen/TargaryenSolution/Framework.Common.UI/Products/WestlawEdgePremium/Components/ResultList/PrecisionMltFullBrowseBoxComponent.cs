namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.ResultList
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Precision MLT blue box component
    /// </summary>
    public class PrecisionMltFullBrowseBoxComponent : PrecisionFullBrowseBoxComponent
    {
        private static readonly By BrowseBoxBadgesLocator = By.XPath(".//div[@class ='BrowseBox-badge-wrapper']//span");
        private static readonly By ContainerLocator = By.XPath(".//div[@class='Athens-browseBox']");
        private static readonly By LegalIssueButtonLocator = By.XPath(".//span[contains(@class,'premium24_legalIssue')]");
        private static readonly By FactPatternButtonLocator = By.XPath(".//span[contains(@class,'premium24_factPattern')]");
        private static readonly By CauseOfActionButtonLocator = By.XPath(".//span[contains(@class,'premium24_causeOfAction')]");
        private static readonly By MotionTypeButtonLocator = By.XPath(".//span[contains(@class,'premium24_motionType')]");
        private static readonly By GoverningLawButtonLocator = By.XPath(".//span[contains(@class,'premium24_governingLaw')]");
        private static readonly By IndustryTypeButtonLocator = By.XPath(".//span[contains(@class,'premium24_industry')]");
        private static readonly By PartyTypeButtonLocator = By.XPath(".//span[contains(@class,'premium24_partyType')]");
        private static readonly By HideButtonLocator = By.XPath(".//span[contains(text(),'Hide')]");
        private static readonly By ShowButtonLocator = By.XPath(".//span[contains(text(),'Show')]");
        private static readonly By InfoBoxLocator = By.XPath("//div[contains(@class, 'BrowseBox-badgeInfo')]");
        
        private IWebElement ContainerElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionMltFullBrowseBoxComponent"/> class.
        /// </summary>
        /// <param name="containerElement"></param>
        public PrecisionMltFullBrowseBoxComponent(IWebElement containerElement) : base(containerElement)
        {
            this.ContainerElement = containerElement;
        }

        /// <summary>
        /// Athens  Browse Box Badges buttons
        /// </summary>
        public IReadOnlyCollection<IButton> BrowseBoxBadgesList => new ElementsCollection<Button>(this.ContainerElement, BrowseBoxBadgesLocator).ToList();

        /// <summary>
        /// Legal issue button
        /// </summary>
        public IButton LegalIssueButton => new Button(this.ContainerElement, LegalIssueButtonLocator);

        /// <summary>
        /// Fact pattern button
        /// </summary>
        public IButton FactPatterButton => new Button(this.ContainerElement, FactPatternButtonLocator);

        /// <summary>
        /// Cause of Action button
        /// </summary>
        public IButton CauseOfActionButton => new Button(this.ContainerElement, CauseOfActionButtonLocator);

        /// <summary>
        /// Motion Type button
        /// </summary>
        public IButton MotionTypeButton => new Button(this.ContainerElement, MotionTypeButtonLocator);

        /// <summary>
        /// Governing Law button
        /// </summary>
        public IButton GoverningLawButton => new Button(this.ContainerElement, GoverningLawButtonLocator);

        /// <summary>
        /// Industry Type button
        /// </summary>
        public IButton IndustryTypeButton => new Button(this.ContainerElement, IndustryTypeButtonLocator);

        /// <summary>
        /// Party Type button
        /// </summary>
        public IButton PartyTypeButton => new Button(this.ContainerElement, PartyTypeButtonLocator);

        /// <summary>
        /// Hide button
        /// </summary>
        public IButton HideButton => new Button(this.ContainerElement, HideButtonLocator);

        /// <summary>
        /// Show button
        /// </summary>
        public IButton ShowButton => new Button(this.ContainerElement, ShowButtonLocator);

        /// <summary>
        /// Info box when we click to any button like Legal Issue, Motion Type and so on
        /// </summary>
        public IInfoBox InfoBox => new InfoBox(InfoBoxLocator);

        /// <summary>
        /// Is MLT full browse box displayed
        /// </summary>
        /// <returns>True - if blue box is displayed, false - otherwise</returns>
        public new bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}