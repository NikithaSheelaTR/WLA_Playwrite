namespace Framework.Common.UI.Products.Shared.Components.Folder
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The Folder Analysis right pane component on RO page. Appears as part of smart folders functionality
    /// </summary>
    public class FolderAnalysisRightPaneComponent : BaseModuleRegressionComponent
    {
        private const string LegalIssueLctMask = ".//ul[@id='legal_issues']/li[contains(.,'{0}')]";

        private static readonly By FolderAnalysisTitleLocator = By.Id("issueBreakdownTitle");

        private static readonly By GraphBarIconLocator = By.CssSelector("#issueBreakdownTitle .icon_graph");

        private static readonly By LegalIssueBoxLocator = By.CssSelector("#legal_issues>li");

        private static readonly By LegalIssueLocator = By.ClassName("co_issueHeader");

        private static readonly By LegalIssueDescriptionLocator =
            By.XPath(".//div[contains(@class,'co_legalIssueDescription')]/a");

        private static readonly By LegalIssueRecommendedDocumentsLocator = By.ClassName("co_recommendedDocs");

        private static readonly By RecommendedDocumentsCountLocator =
            By.XPath(".//div[@class='co_recommendedDocs']//localization");

        private static readonly By RecommendedDocumentsLinkLocator = By.CssSelector(".co_resultList .co_viewAllLink");

        private static readonly By SpinnerLocator = By.ClassName("co_progressIndicator");

        private static readonly By ContainerLocator = By.Id("smartFoldersDirectiveContainer");

        private EnumPropertyMapper<LegalIssue, BaseTextModel> legalIssueMap;

        /// <summary>
        /// Gets the LegalIssue enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<LegalIssue, BaseTextModel> LegalIssueMap
            => this.legalIssueMap = this.legalIssueMap ?? EnumPropertyModelCache.GetMap<LegalIssue, BaseTextModel>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Return legal issue description
        /// </summary>
        /// <param name="issue">The <see cref="LegalIssue"/></param>
        /// <returns>The new instance of T page</returns>
        public T ClickLegalIssueDescription<T>(LegalIssue issue) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(this.GetLegalIssueElement(issue), LegalIssueDescriptionLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click recommended documents link and go to the Folder Analysis page
        /// </summary>
        /// <returns>The new instance of T page</returns>
        public T ClickRecommendedDocumentsLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementNotDisplayed(SpinnerLocator);
            DriverExtensions.ScrollToTop();
            DriverExtensions.Click(DriverExtensions.WaitForElement(RecommendedDocumentsLinkLocator));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Count displayed legal issues on the page
        /// </summary>
        /// <returns>Legal issue count</returns>
        public int GetLegalIssuesCount() => DriverExtensions.GetElements(LegalIssueBoxLocator).Count;

        /// <summary>
        /// Return legal issue description
        /// </summary>
        /// <param name="issue">The <see cref="LegalIssue"/></param>
        /// <returns>Description</returns>
        public string GetLegalIssueDescription(LegalIssue issue) =>
            DriverExtensions.WaitForElement(this.GetLegalIssueElement(issue), LegalIssueDescriptionLocator).Text;

        /// <summary>
        /// Get recommended documents link count
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetRecommendedDocumentsCount() =>
            int.Parse(DriverExtensions.GetElement(RecommendedDocumentsLinkLocator, By.XPath("./localization")).GetAttribute("formatkeyvalue-doccount"));

        /// <summary>
        /// Checks if all legal issue boxes has recommended docs
        /// </summary>
        /// <returns>True if </returns>
        public bool HaveAllLegalIssuesRecommedations() =>
            DriverExtensions.GetElements(LegalIssueBoxLocator)
            .All(e => int.Parse(DriverExtensions.WaitForElement(
                e, RecommendedDocumentsCountLocator).GetAttribute("formatkeyvalue-doccount")) > 0);

        /// <summary>
        /// Verify that graph bar icon is displayed on the page
        /// </summary>
        /// <returns>True, if graph bar is displayed</returns>
        public bool IsGraphBarIconDisplayed() => DriverExtensions.IsDisplayed(GraphBarIconLocator, 5);

        /// <summary>
        /// Verify that graph bar icon is not clickable on the page
        /// </summary>
        /// <returns>True, if graph bar is not clickable</returns>
        public bool IsGraphBarIconNotClickable() =>
            DriverExtensions.WaitForElement(FolderAnalysisTitleLocator).GetAttribute("href") == null;

        /// <summary>
        /// Verify that Folder Analysis label is displayed
        /// </summary>
        /// <returns>True, if graph bar is not clickable</returns>
        public bool IsFolderAnalysisLabelDisplayed() =>
            DriverExtensions.IsDisplayed(FolderAnalysisTitleLocator, 5)
            && DriverExtensions.GetText(FolderAnalysisTitleLocator).ToLower().Contains("folder analysis");

        /// <summary>
        /// Verify that recommended documents link is displayed on the page
        /// </summary>
        /// <returns>True, if recommended documents link is displayed</returns>
        public bool IsRecommendedDocumentsLinkDisplayed() =>
            DriverExtensions.IsDisplayed(RecommendedDocumentsLinkLocator, 5);

        /// <summary>
        /// Verify that recommended documents link is  clickable on the page
        /// </summary>
        /// <returns>True, if recommended documents link is clickable</returns>
        public bool IsRecommendedDocumentsLinkClickable() =>
            DriverExtensions.WaitForElement(RecommendedDocumentsLinkLocator).GetAttribute("href") != null;

        /// <summary>
        /// Verify that legal issue label is displayed
        /// </summary>
        /// <param name="issue">The <see cref="LegalIssue"/></param>
        /// <returns>True, if legal issue label is displayed</returns>
        public bool IsLegalIssueLabelDisplayed(LegalIssue issue) =>
            DriverExtensions.WaitForElement(this.GetLegalIssueElement(issue), LegalIssueLocator).Text == this.LegalIssueMap[issue].Text;

        /// <summary>
        /// Verify that legal issue text is clickable 
        /// </summary>
        /// <param name="issue">The <see cref="LegalIssue"/></param>
        /// <returns>True, if  legal issue text is clickable</returns>
        public bool IsLegalIssueTextClickable(LegalIssue issue) =>
            DriverExtensions.WaitForElement(this.GetLegalIssueElement(issue), LegalIssueDescriptionLocator).GetAttribute("href") != null;

        /// <summary>
        /// Verify that legal issue recommended document is not clickable 
        /// </summary>
        /// <param name="issue">The <see cref="LegalIssue"/></param>
        /// <returns>True, if legal issue recommended document is not clickable</returns>
        public bool IsLegalIssueRecommendedDocumentNotClickable(LegalIssue issue) =>
            DriverExtensions.WaitForElement(this.GetLegalIssueElement(issue), LegalIssueRecommendedDocumentsLocator).GetAttribute("href") == null;

        /// <summary>
        /// Returns legal issue
        /// </summary>
        /// <param name="issue">The <see cref="LegalIssue"/></param>
        /// <returns>Legal issue WebElement</returns>
        protected IWebElement GetLegalIssueElement(LegalIssue issue) =>
            DriverExtensions.WaitForElement(By.XPath(string.Format(LegalIssueLctMask, this.LegalIssueMap[issue].Text)));
    }
}
