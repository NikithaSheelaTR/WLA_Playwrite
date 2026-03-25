namespace Framework.Common.UI.Products.WestlawEdge.Components.SearchQuestionAnswerComponent
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The teaser component.
    /// </summary>
    public class EdgeSearchQuestionAnswerComponent : BaseModuleRegressionComponent
    {
        private const string FolderLctMask = "//span[contains(text(), '{0}')]";

        private static readonly By CollapseButtonLocator = By.Id("qa_minimize");

        private static readonly By ExpandButtonLocator = By.Id("qa_maximize");

        private static readonly By JurisdictionLocator = By.ClassName("co_searchResults_jurisdiction");

        private static readonly By QuestionLocator = By.ClassName("co_searchResults_question");

        private static readonly By ViewMoreButtonLocator = By.XPath("//a[contains(@id,'co_idMoreQA')]");

        private static readonly By ContainerLocator = By.XPath("//div[contains(@class,'co_searchResults_summaryQA')]");

        private static readonly By AssociatedContentLocator = By.ClassName("co_statutoryCitation");

        private static readonly By CasesTabLocator = By.XPath("//li[contains(@id,'Cases0')]");

        private static readonly By StatutesTabLocator = By.XPath("//li[contains(@aria-controls,'Statutes-tab')]");

        private static readonly By SecondarySourcesTabLocator = By.XPath("//li[contains(@aria-controls,'SecondarySources-tab')]");
       
        private static readonly By CasesContentTypeLocator = By.Id("co_contentTypeLinksBox");
        
        private static readonly By StatutesContentTypeLocator = By.XPath("//a[@id='co_search_contentNav_link_STATUTE']");

        private static readonly By BackToSearchLocator = By.XPath("//span[contains(.,'Back to Search')]");

        private static readonly By SaveButttonLocator = By.XPath("//input[@value='Save']");

        private static readonly By DefaultFolderLocator = By.XPath("//a[contains(@class,'co_tree_expand')]");
        
        private static readonly By NestedFolderLocator = By.XPath("//SPAN[@class='co_tree_name_span'][text()='StaticQA']");

        /// <summary>
        /// Show/View more button
        /// </summary>
        public IButton ViewMoreButton => new Button(ViewMoreButtonLocator);

        /// <summary>
        /// Expand button
        /// </summary>
        public IButton ExpandButton => new Button(ExpandButtonLocator);

        /// <summary>
        /// Collapse button
        /// </summary>
        public IButton CollapseButton => new Button(CollapseButtonLocator);

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButttonLocator);

        /// <summary>
        /// Default Folder
        /// </summary>
        public IButton DefaultFolder => new Button(DefaultFolderLocator);

        /// <summary>
        /// Nested Folder
        /// </summary>
        public IButton NestedFolder => new Button(NestedFolderLocator);

        /// <summary>
        /// Folder
        /// </summary>
        public IButton Folder (string folderName)
            => new Button(By.XPath(string.Format(FolderLctMask, folderName)));            
        
        /// <summary>
        /// The Question and Answer Result list component
        /// </summary>
        public EdgeSearchQuestionAnswerResultListComponent QnAResultList { get; } = new EdgeSearchQuestionAnswerResultListComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Returns question text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetQuestionText() => DriverExtensions.GetText(QuestionLocator);

        /// <summary>
        /// Returns the Applicable jurisdiction for the provided answer
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetAnswersJurisdictionText() => DriverExtensions.GetText(JurisdictionLocator).TrimStart('-', ' ');

        /// <summary>
        /// Cases Tab
        /// </summary>
        public string AssociatedContent() => DriverExtensions.GetText(AssociatedContentLocator);

        /// <summary>
        /// Back to Search
        /// </summary>
        public IButton BackToSearch => new Button(BackToSearchLocator);

        /// <summary>
        /// Cases Tab
        /// </summary>
        public IButton CasesTab => new Button(CasesTabLocator);

        /// <summary>
        /// Statutes Tab
        /// </summary>
        public IButton StatutesTab => new Button(StatutesTabLocator);

        /// <summary>
        /// Secondary Sources Tab Tab
        /// </summary>
        public IButton SecondarySourcesTab => new Button(SecondarySourcesTabLocator);

        /// <summary>
        /// Active Statutes Tab
        /// </summary>
        public bool StatutesTabActive() => DriverExtensions.WaitForElement(StatutesTabLocator).GetAttribute("class").Contains("active");

        /// <summary>
        /// Active Secondary Sources Tab
        /// </summary>
        public bool SecondarySourcesTabActive() => DriverExtensions.WaitForElement(SecondarySourcesTabLocator).GetAttribute("class").Contains("active");

        /// <summary>
        /// Cases Content Type
        /// </summary>
        public string CasesContentType() => DriverExtensions.GetText(CasesContentTypeLocator);

        /// <summary>
        /// Statutes Content
        /// </summary>
        public IButton StatutesContent => new Button(StatutesContentTypeLocator);

        /// <summary>
        /// Expand QAndA section
        /// </summary>
        public void ExpandQAndAComponent()
        {
            if (this.ExpandButton.Displayed)
            {
                this.ExpandButton.Click();
            }
        }
    }
}