namespace Framework.Common.UI.Products.Shared.Pages.AdvancedSearchTemplates
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.AdvancedSearch;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// FormsAstPage
    /// </summary>
    public class FormsAstPage : CommonAdvancedSearchPage
    {
        private static readonly string LimitResultItemSafeLocator =
            "//li[@id='co_search_advancedSearch_listItem_VIEW']//label[text()={0}]";

        private static readonly By TopicSelectionsLinkLocator = By.Id("co_search_advancedSearch_VW_link");

        /// <summary>
        /// ClickTopicSelectionsLink
        /// </summary>
        /// <returns> The <see cref="TopicSelectionsDialog"/>. </returns>
        public TopicSelectionsDialog ClickTopicSelectionsLink()
        {
            DriverExtensions.WaitForElement(TopicSelectionsLinkLocator).Click();
            return new TopicSelectionsDialog();
        }

        /// <summary>
        /// SelectLimitResultCheckBoxesByName
        /// </summary>
        /// <param name="names">name</param>
        public void SelectLimitResultCheckBoxesByName(params string[] names) => names.ToList().
            ForEach(name => DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(LimitResultItemSafeLocator, name)).Click());
    }
}