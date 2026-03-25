namespace Framework.Common.UI.Products.WestLawNext.Components.Folders
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The Legal issue left pane component on Folder Analysis page. Appeares as part of smart folders functionality
    /// </summary>
    public class LegalIssueComponent : BaseModuleRegressionComponent
    {
        private const string LegalIssueLctMask = ".//li[contains(@class,'co_legalIssue')][.//a[contains(@title, '{0}')]]";

        private const string SmartFolderOptionLctMask = "//div[@id='leftPane']//label[contains(., '{0}')]";

        private static readonly By LegalIssuesLabelLocator = By.XPath("//div[@id='leftPane']//h1[@class='ng-binding']");

        private static readonly By ContainerLocator = By.CssSelector("#smartFoldersMainContent #leftPane");

        private EnumPropertyMapper<SmartFoldersDocumentType, WebElementInfo> smartFoldersDocumentTypeMap;

        /// <summary>
        /// Gets the SmartFoldersDocumentType enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<SmartFoldersDocumentType, WebElementInfo> SmartFoldersDocumentTypeMap
            => this.smartFoldersDocumentTypeMap = this.smartFoldersDocumentTypeMap
            ?? EnumPropertyModelCache.GetMap<SmartFoldersDocumentType, WebElementInfo>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Verify legal issue is selected
        /// </summary>
        /// <param name="issue">Issue description</param>
        public void ClickLegalIssue(string issue)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(LegalIssueLctMask, issue))).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Gets Count of smart folders document options
        /// </summary>
        /// <param name="type">The <see cref="SmartFoldersDocumentType"/>.</param>
        /// <returns>The <see cref="SmartFoldersDocumentType"/></returns>
        public int GetDocumentsCount(SmartFoldersDocumentType type)
        {
            string text = DriverExtensions.GetText(By.XPath(string.Format(SmartFolderOptionLctMask, this.SmartFoldersDocumentTypeMap[type].Text))).Remove(0, 23).Replace(')', ' ').Trim();

            int count;
            return int.TryParse(text, out count) ? count : 0;
        }

        /// <summary>
        /// Verifies option displayed
        /// </summary>
        /// <param name="type">The <see cref="SmartFoldersDocumentType"/>.</param>
        /// <returns>True if option is displayed, false otherwise</returns>
        public bool IsOptionDisplayed(SmartFoldersDocumentType type)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(SmartFolderOptionLctMask, this.SmartFoldersDocumentTypeMap[type].Text)), 5);

        /// <summary>
        /// Verifies is title displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsTitleDisplayed()
        {
            IWebElement legalIssuesLabel = DriverExtensions.WaitForElement(LegalIssuesLabelLocator);
            return legalIssuesLabel.Text.Equals("Legal Issues") && legalIssuesLabel.Displayed;
        }

        /// <summary>
        /// Verify legal issue is selected
        /// </summary>
        /// <param name="issue">Issue description</param>
        /// <returns>True if issue is selected</returns>
        public bool IsLegalIssueDisplayed(string issue) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(LegalIssueLctMask, issue)), 5);

        /// <summary>
        /// Verify legal issue is selected
        /// </summary>
        /// <param name="issue">Issue description</param>
        /// <returns>True if issue is selected</returns>
        public bool IsLegalIssueSelected(string issue) =>
            DriverExtensions.WaitForElement(By.XPath(string.Format(LegalIssueLctMask, issue))).GetAttribute("class").Contains("co_selected");

        /// <summary>
        /// Clicks SmartFoldersDocumentType option radio button
        /// </summary>
        /// <param name="type">The <see cref="SmartFoldersDocumentType"/>.</param>
        public void SelectDocumentOption(SmartFoldersDocumentType type)
            => DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(SmartFolderOptionLctMask, this.SmartFoldersDocumentTypeMap[type].Text))).Click();
    }
}