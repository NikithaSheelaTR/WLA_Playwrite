namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdgePremium.DropDowns;
    using Framework.Common.UI.Products.Shared.Dialogs;

    /// <summary>
    /// CoCounselChatAssistantDialog
    /// </summary>
    public class CoCounselChatAssistantDialog : BaseModuleRegressionDialog
    {         
        private static readonly By ContainerLocator = By.XPath("//div[@id='coid_ai_assistant']//div");
        private static readonly By ExpandButtonLocator = By.XPath(".//saf-button[@aria-label='CoCounsel AI Assistant']");
        private static readonly By MaximizeButtonLocator = By.XPath(".//saf-button[@aria-label='Maximize CoCounsel AI Assistant']");
        private static readonly By CollapseButtonLocator = By.XPath(".//saf-button[@aria-label='CoCounsel AI Assistant']");
        private static readonly By MinimizeButtonLocator = By.XPath(".//saf-button[@aria-label='Minimize CoCounsel AI Assistant']");
        private static readonly By FeedbackYesButtonLocator = By.XPath(".//saf-button[@data-testid='thumbs-up-button']");
        private static readonly By FeedbackNoButtonLocator = By.XPath(".//saf-button[@data-testid='thumbs-down-button']");
        private static readonly By MenuButtonLocator = By.XPath(".//saf-button[@class='toggle-button']");

        /// <summary>
        /// Tray dropdown.
        /// </summary>
        public TrayDropdown MenuDropdown => new TrayDropdown(this.ComponentLocator);

        /// <summary>
        /// Chat
        /// </summary>
        public CoCounselChatComponent Chat { get; } = new CoCounselChatComponent();

        /// <summary>
        /// CoCounsel All Cited Sources
        /// </summary>
        public CoCounselAllCitedSourcesComponent CoCounselAllCitedSources { get; } = new CoCounselAllCitedSourcesComponent();

        /// <summary>
        /// CoCounsel Recent Chat History
        /// </summary>
        public CoCounselRecentChatHistoryComponent CoCounselRecentChatHistory { get; } = new CoCounselRecentChatHistoryComponent();

        /// <summary>
        /// Expand button
        /// </summary>
        public IButton ExpandButton => new Button(this.ComponentLocator, ExpandButtonLocator);

        /// <summary>
        /// Maximize button
        /// </summary>
        public IButton MaximizeButton => new Button(this.ComponentLocator, MaximizeButtonLocator);

        /// <summary>
        /// Collapse button
        /// </summary>
        public IButton CollapseButton => new Button(this.ComponentLocator, CollapseButtonLocator);

        /// <summary>
        /// Minimize button
        /// </summary>
        public IButton MinimizeButton => new Button(this.ComponentLocator, MinimizeButtonLocator);

        /// <summary>
        /// Feedback Yes button
        /// </summary>
        public IButton FeedbackYesButton => new Button(this.ComponentLocator, FeedbackYesButtonLocator);

        /// <summary>
        /// Feedback No button
        /// </summary>
        public IButton FeedbackNoButton => new Button(this.ComponentLocator, FeedbackNoButtonLocator);

        /// <summary>
        /// Menu button
        /// </summary>
        public IButton MenuButton => new Button(this.ComponentLocator, MenuButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get dialog attribute
        /// </summary>
        public string GetAttribute(string attribute) => DriverExtensions.GetElement(ContainerLocator).GetAttribute(attribute);
    }
}
