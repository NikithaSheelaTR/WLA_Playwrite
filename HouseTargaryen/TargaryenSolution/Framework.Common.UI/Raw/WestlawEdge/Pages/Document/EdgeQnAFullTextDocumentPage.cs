namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using Framework.Common.UI.Products.WestlawEdge.Components.SearchQuestionAnswerComponent;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Question And Answer Full Text Document Page.
    /// </summary>
    public class EdgeQnAFullTextDocumentPage : EdgeCommonDocumentPage
    {
        private static readonly By QuestionLocator = By.ClassName("co_searchResults_question");
        private static readonly By JurisdictionForEasyQaLocator = By.XPath("//div[@id='co_docHeaderContainer']//span[contains(@class,'co_qaJurisdiction ng-binding')]");
        private static readonly By JurisdictionForStaticQaLocator = By.XPath("//div[@id='co_docHeaderContainer']//span[contains(@class,'co_searchResults_jurisdiction')]");
        private static readonly By SelectedJurisdictionLocator = By.Id("jurisdictionIdInner");
        private static readonly By FolderMessageLocator = By.XPath("//div[contains(@class,'co_foldering_popupMessageContainer')]//div[contains(@class,'co_infoBox_message')]");
        private static readonly By ExpandCollapseAnswersLocator = By.Id("qa_maximizeMinimize");

        /// <summary>
        /// The Question and Answer Result list component
        /// </summary>
        public EdgeSearchQuestionAnswerResultListComponent QnAResultList { get; } = new EdgeSearchQuestionAnswerResultListComponent();

        /// <summary>
        /// Returns the question text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetQuestionText() => DriverExtensions.GetText(QuestionLocator);

        /// <summary>
        /// Expands to see answer from other jurisdiction
        /// </summary>
        public void ExpandAnswerFromOtherJurisdiction() => DriverExtensions.Click(ExpandCollapseAnswersLocator);

        /// <summary>
        /// Gets Folder Message
        /// </summary>
        /// <returns></returns>
        public string GetFolderMessage() => DriverExtensions.WaitForElement(FolderMessageLocator).Text.Trim();

        /// <summary>
        /// Returns jurisdiction for easy qa
        /// </summary>
        /// <returns></returns>
        public string GetJurisdictionForEasyQaText() => DriverExtensions.GetText(JurisdictionForEasyQaLocator);

        /// <summary>
        /// Returns jurisdiction for easy qa
        /// </summary>
        /// <returns></returns>
        public string GetJurisdictionForStaticQaText() => DriverExtensions.GetText(JurisdictionForStaticQaLocator);

        /// <summary>
        /// Gets name of selected juris
        /// </summary>
        /// <returns></returns>
        public string GetSelectedJurisdiction() => DriverExtensions.GetText(SelectedJurisdictionLocator);
    }
}