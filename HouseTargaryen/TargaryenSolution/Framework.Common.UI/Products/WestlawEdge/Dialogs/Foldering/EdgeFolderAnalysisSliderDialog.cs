namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Extensions;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// Folder Analysis slider
    /// </summary>
    public class EdgeFolderAnalysisSliderDialog : BaseModuleRegressionDialog
    {
        private const string LegalIssueLctMask = ".//ul[@id='legal_issues']/li[contains(.,'{0}')]";
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class, 'FolderAnalysisSliderWrapper ')]//div[@class='co_contentSliderContent']");
        private static readonly By HeaderLocator = By.XPath(".//div[@class = 'co_contentSliderHeader']");
        private static readonly By NoRecommendationsLabelLocator = By.XPath(".//div[@class = 'co_noResultsSection']");
        private static readonly By NewRecommendationsIconLocator = By.CssSelector("#coid_folderAnalysisRecommendations_newRecommendedDocs_issue localization");
        private static readonly By LegalIssueDescriptionLocator = By.XPath(".//div[contains(@class,'co_legalIssueDescription')]/a");
        private static readonly By RecommendedDocumentsLinkLocator = By.CssSelector(".co_resultList .co_viewAllLink");
        private static readonly By SpinnerLocator = By.XPath(".//div[@id= 'loadingSpinner']");
        private static readonly By CloseButtonLocator = By.XPath("//button[@class= 'co_widget_closeIcon']");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeFolderAnalysisSliderDialog"/> class. 
        /// Constructor
        /// </summary>
        public EdgeFolderAnalysisSliderDialog()
        {
            DriverExtensions.WaitForElementDisplayed(this.Container, HeaderLocator);
            DriverExtensions.WaitForElementNotDisplayed(this.Container, SpinnerLocator);
        }

        /// <summary>
        /// No recommendations label
        /// </summary>
        public ILabel NoRecommendationsLabel => new Label(this.Container, NoRecommendationsLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(this.Container,CloseButtonLocator);

        /// <summary>
        /// Gets the LegalIssue enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<LegalIssue, BaseTextModel> LegalIssueMap
            => EnumPropertyModelCache.GetMap<LegalIssue, BaseTextModel>();

        private IWebElement Container => DriverExtensions.WaitForElement(By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.FolderAnalysisSliderDialog].LocatorString));

        /// <summary>
        /// The get number of new recommendations for issue.
        /// </summary>
        /// <param name="issue">
        /// The issue.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetNumberOfNewRecommendationsForIssue(LegalIssue issue)
            =>
                DriverExtensions.IsDisplayed(NewRecommendationsIconLocator, 5)
                    ? DriverExtensions.WaitForElement(this.GetLegalIssueElement(issue), NewRecommendationsIconLocator)
                                      .Text.ConvertCountToInt()
                    : 0;

        /// <summary>
        /// Return legal issue description
        /// </summary>
        /// <param name="issue">The <see cref="LegalIssue"/></param>
        /// <returns>The new instance of T page</returns>
        public T ClickLegalIssueDescription<T>(LegalIssue issue) where T : ICreatablePageObject
        {
            DriverExtensions.ScrollToElementInsideContainer(ContainerLocator, By.XPath(string.Format(LegalIssueLctMask, this.LegalIssueMap[issue].Text)));
            DriverExtensions.WaitForElement(this.GetLegalIssueElement(issue), LegalIssueDescriptionLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click recommended documents link and go to the Folder Analysis page
        /// </summary>
        /// <returns>The new instance of T page</returns>
        public T ClickRecommendedDocumentsLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.ScrollToTop();
            DriverExtensions.Click(DriverExtensions.WaitForElement(RecommendedDocumentsLinkLocator));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Returns legal issue
        /// </summary>
        /// <param name="issue">The <see cref="LegalIssue"/></param>
        /// <returns>Legal issue WebElement</returns>
        protected IWebElement GetLegalIssueElement(LegalIssue issue) =>
            DriverExtensions.WaitForElement(By.XPath(string.Format(LegalIssueLctMask, this.LegalIssueMap[issue].Text)));
    }
}