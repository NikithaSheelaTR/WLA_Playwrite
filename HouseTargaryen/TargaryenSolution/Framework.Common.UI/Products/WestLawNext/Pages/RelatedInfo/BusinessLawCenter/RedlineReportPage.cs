namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.BusinessLawCenter
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.TableOfContents;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Redline Comparison Page (Business Law Center)
    /// </summary>
    public class RedlineReportPage : CommonDocumentPage
    {
        private static readonly By TitleLocator = By.Id("title");

        private static readonly By SaveComparisonButtonLocator = By.XPath("//a[@title='Save Comparison']");

        private static readonly By InsertionLabelLocator = By.XPath("//span[@class='co_redlineAdd co_redlineDifference']");

        private static readonly By DeletionLabelLocator = By.XPath("//span[@class='co_redlineDelete co_redlineDifference']");
        
        /// <summary>
        /// Toolbar
        /// </summary>
        public new RedlineToolbarComponent Toolbar { get; set; } = new RedlineToolbarComponent();

        /// <summary>
        /// Verify that page title is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsTitleDisplayed() => DriverExtensions.IsDisplayed(TitleLocator);

        /// <summary>
        /// Get report title
        /// </summary>
        /// <returns> Report title </returns>
        public string GetReportTitle() => DriverExtensions.GetText(TitleLocator);

        /// <summary>
        /// Click 'Save Comparison' button
        /// </summary>
        /// <returns> The <see cref="RedliningComparisonToolDialog"/>. </returns>
        public T ClickSaveComparisonButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SaveComparisonButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Count of Insertion labels
        /// </summary>
        /// <returns> Insertion labels count </returns>
        public int GetInsertionLabelCount() => DriverExtensions.GetElements(InsertionLabelLocator).Count;

        /// <summary>
        /// Count of Deletion label
        /// </summary>
        /// <returns> Deletion labels count </returns>
        public int GetDeletionLabelCount() => DriverExtensions.GetElements(DeletionLabelLocator).Count;
    }
}
