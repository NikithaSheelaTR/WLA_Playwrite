namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Research Tab Panel
    /// </summary>
    public class PrecisionResearchTabPanel : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='panel2']");
        private static readonly By StartPrecisionResearchButtonLocator = By.XPath(".//*[contains(@class,'Button-primary')]");
        private static readonly By ViewTopicDetailsButtonLocator = By.XPath(".//button[contains(@class, 'Athens-columnThree-viewTopicButton')]");   
        private static readonly By LearnMoreLinkLocator = By.XPath(".//button[contains(@class,'Athens-learnMoreResearchButton')]");
        private static readonly By AvailableTopicsButtonsLocator = By.XPath(".//div[@class='Athens-marketing-container-columnThree']//button[@class='co_linkBlue']");
        private static readonly By PrecisionAttributesLabelLocator = By.XPath(".//div[@class='Athens-marketing-container-columnOne-info']");
        private static readonly By WatchTheVideoButtonLocator = By.XPath(".//button[@class='Athens-marketing-watchVideo']");
        private static readonly By SelectTopicsButtonLocator = By.XPath(".//button[@aria-label='Select Precision Research topics']");
        private static readonly By ChevronButtonLocator = By.XPath(".//button[@class='Athens-columnThree-toggleButton']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Precision Research";

        /// <summary>
        /// Precision attributes label
        /// </summary>
        public ILabel PrecisionAttributesLabel => new Label(this.ComponentLocator, PrecisionAttributesLabelLocator);

        /// <summary>
        /// Start Precision Research button
        /// </summary>
        public IButton StartPrecisionResearchButton => new Button(this.ComponentLocator, StartPrecisionResearchButtonLocator);

        /// <summary>
        /// View topic details button
        /// </summary>
        public IButton ViewTopicDetailsButton => new Button(this.ComponentLocator, ViewTopicDetailsButtonLocator);

        /// <summary>
        /// Watch the video button
        /// </summary>
        public IButton WatchTheVideoButton => new Button(this.ComponentLocator, WatchTheVideoButtonLocator);

        /// <summary>
        /// Select topics button
        /// </summary>
        public IButton SelectTopicsButton => new Button(this.ComponentLocator, SelectTopicsButtonLocator);

        /// <summary>
        /// Chevron button
        /// </summary>
        public IButton ChevronButton => new Button(this.ComponentLocator, ChevronButtonLocator);

        /// <summary>
        /// Buttons under 'Available topics'
        /// </summary>
        public IReadOnlyCollection<IButton> AvailableTopicsButtonsList => new ElementsCollection<Button>(this.ComponentLocator, AvailableTopicsButtonsLocator);

        /// <summary>
        /// Learn more link
        /// </summary>
        public ILink LearnMoreLink => new Link(this.ComponentLocator, LearnMoreLinkLocator);
    }
}
