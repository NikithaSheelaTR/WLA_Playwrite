namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Jurisdiction Selection Dialog
    /// </summary>
    public class JurisdictionSelectingDialog : BaseModuleRegressionDialog
    {
        /// <summary>
        /// Container Locator
        /// </summary>
        protected static By ContainerLocator => By.Id("co_la_modalBodyFocus");

        private static readonly By ItemLocator = By.XPath("//div[@id = 'co_la_modalBodyFocus']//li[@role = 'treeitem']/label[@class= 'co_collector-labelWrapper']");

        /// <summary>
        /// Jurisdiction selecting Dialog
        /// </summary>
        public JurisdictionSelectingDialog()
        {
        }

        /// <summary>
        /// ResultList
        /// </summary>
        public List<JurisdictionItem> TypeaheadItems => new ItemsCollection<JurisdictionItem>(ContainerLocator, ItemLocator).ToList();
    }
}