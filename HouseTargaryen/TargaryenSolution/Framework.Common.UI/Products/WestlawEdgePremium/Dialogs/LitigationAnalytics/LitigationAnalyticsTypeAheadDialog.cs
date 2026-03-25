namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// LitigationAnalyticsTypeAheadDialog
    ///  </summary>
    public class LitigationAnalyticsTypeAheadDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id = 'contentTypeDetailsContainer']");
        private static readonly By ItemLocator = By.XPath(".//a[contains(@class,'SearchSuggestionsSection SearchSuggestionWestlawAnswer')]");

        /// <summary>
        /// Litigation Analytics Type Ahead Dialog
        /// </summary>
        public LitigationAnalyticsTypeAheadDialog()
        { }

        /// <summary>
        /// ResultList
        /// </summary>
        public List<LitigationAnalyticsTypeaheadItem> TypeaheadItems => new ItemsCollection<LitigationAnalyticsTypeaheadItem>(this.Container, ItemLocator).Where(item => item.IsCurrentItemDisplayed()).ToList();

        private IWebElement Container => DriverExtensions.GetElement(ContainerLocator);
    }
}