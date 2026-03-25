namespace Framework.Common.UI.Raw.WestlawEdge.Pages.Document
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// EdgeExpertQuestionsAndAnswersPage
    /// </summary>
    public class EdgeExpertQuestionsAndAnswersPage : EdgeCommonDocumentPage
    {
        private static readonly By ExpertLinkLocator = By.Id("co_link_ID0ETB");

        /// <summary>
        /// Returns the link value for Expert.
        /// </summary>
        /// <returns> Expert Link value </returns>
        public string GetExpertQuestionAndAnswerLinkValue() => DriverExtensions.WaitForElement(ExpertLinkLocator).GetAttribute("data-href");
    }
}
