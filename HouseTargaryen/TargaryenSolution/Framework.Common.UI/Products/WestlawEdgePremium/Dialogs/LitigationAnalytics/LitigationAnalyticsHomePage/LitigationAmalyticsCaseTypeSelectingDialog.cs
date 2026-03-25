namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Case Type selecting dialog.
    /// </summary>
    public class LitigationAnalyticsCaseTypeSelectingDialog : BaseModuleRegressionDialog
    {
        /// <summary>
        /// Container Locator
        /// </summary>
        protected static By ContainerLocator => By.Id("co_la_modalBodyFocus");

        private static readonly By ItemLocator = By.XPath("//div[@id = 'co_la_modalBodyFocus']//li[@role = 'treeitem']/label[@class= 'co_collector-labelWrapper']");
        private static readonly By SaveButtonLocator = By.XPath("//button[@class = 'co_primaryBtn']");
        private static readonly By SearchInputCaseTypeDialogLocator = By.Id("co_facet_searchBoxInput");
        private static readonly By TypeaheadCaseTypeDialogLocator = By.XPath(".//div[@class='co_overlayBox_topLeft']//*[contains(text(),'Case type')]");

        /// <summary>
        /// Litigation Analytics Case Type Selecting Dialog
        /// </summary>
        public LitigationAnalyticsCaseTypeSelectingDialog()
        {
        }

        /// <summary>
        /// Case type result list items.
        /// </summary>
        public List<CaseTypeItem> CaseTypeItems => new ItemsCollection<CaseTypeItem>(ContainerLocator, ItemLocator).ToList();

        /// <summary>
        /// Save button.
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Enter search query
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="searchQuery">Search query</param>
        /// <returns>New instance of the page</returns>
        public T EnterSearchQueryCaseTypeDialog<T>(string searchQuery)
            where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(searchQuery, SearchInputCaseTypeDialogLocator);
            DriverExtensions.WaitForElementDisplayed(TypeaheadCaseTypeDialogLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}