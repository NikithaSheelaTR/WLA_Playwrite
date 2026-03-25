namespace Framework.Common.UI.Products.WestLawNextCanada.Components.AssistedResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// AI-Assisted Research tab panel
    /// </summary>
    public class CanadaAiArTabPanel : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='panel1']");
        private static readonly By JurisdictionTitleLocator = By.XPath("//div[@id='aiAssistantJurisdictionContainer']//p");
        private static readonly By JurisdictionBtnLocator = By.ClassName("Jurisdiction-selector-button");
        private static readonly By QuestionTextboxLocator = By.XPath(".//*[@id='saf-text-area1']");
        private static readonly By SubmitButtonLocator = By.XPath(".//*[contains(@class, 'saf-button_primary')]");
        private static readonly By RecentQuestionBtnLocator = By.XPath("//button[contains(@class,'recent-search-dropdown-button')]");
        private static readonly By RecentQuestionDropdownLabel = By.XPath("//div[@id='coid_recentQueries_dropdownID']/span");
        private static readonly By RecentQuestionLocator = By.XPath("//ul[contains(@class,'CS-recent-search-dropdown-list')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "AI-Assisted Research";

        /// <summary>
        /// Canada Recent Question Items list
        /// </summary>
        /// <returns>List of recent questions</returns>
        public ItemsCollection<CanadaRecentQuestionsItem> CanadaRecentQuestionItems =>
            new ItemsCollection<CanadaRecentQuestionsItem>(this.ComponentLocator, RecentQuestionLocator);

        /// <summary>
        /// Question textbox
        /// </summary>
        public ITextbox QuestionTextbox => new Textbox(this.ComponentLocator, QuestionTextboxLocator);

        /// <summary>
        /// Submit button
        /// </summary>
        public IButton SubmitButton => new Button(this.ComponentLocator, SubmitButtonLocator);

        /// <summary>
        /// Recent Question Dropdown label
        /// </summary>
        public ILabel RecentQuestionDropdown => new Label(this.ComponentLocator, RecentQuestionDropdownLabel);

        /// <summary>
        ///  Jurisdiction title from AI Assisted Research tab label
        /// </summary>
        public ILabel JurisdictionTitle => new Label(this.ComponentLocator, JurisdictionTitleLocator);

        /// <summary>
        /// Jurisdiction Selected Value button
        /// </summary>
        public IButton JurisdictionButton => new Button(this.ComponentLocator, JurisdictionBtnLocator);

        /// <summary>
        /// Jurisdiction Selected Value label
        /// </summary>
        public IButton RecentQuestionButton => new Button(this.ComponentLocator, RecentQuestionBtnLocator);
    }
}
