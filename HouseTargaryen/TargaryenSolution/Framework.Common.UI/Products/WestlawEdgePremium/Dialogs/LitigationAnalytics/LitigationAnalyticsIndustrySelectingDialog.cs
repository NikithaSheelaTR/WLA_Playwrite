namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Litigation Analytics Industry Selecting Dialog
    /// </summary>
    public class LitigationAnalyticsIndustrySelectingDialog : BaseModuleRegressionDialog
    {
        private static readonly By Container = By.XPath("//div[@id = 'co_la_modalBodyFocus']");
        private static readonly By NaiscRadiobuttonLocator = By.XPath("//input[@id = 'naics']");
        private static readonly By SicRadiobuttonLocator = By.XPath("//input[@id = 'sic']");
        private static readonly By IndustrySearchBoxLocator = By.Id("co_facet_searchBoxInput");
        private static readonly By IndustryResultItemLocator = By.ClassName("co_collector-labelWrapper");
        private static readonly By SaveButtonLocator = By.XPath("//button[@class = 'co_primaryBtn']");
        private static readonly By CancelLocator = By.ClassName("co_overlayBox_buttonCancel");

        /// <summary>
        /// Naisc Radiobutton
        /// </summary>
        public IRadiobutton NaiscRadiobutton => new Radiobutton(Container, NaiscRadiobuttonLocator);

        /// <summary>
        /// Sic Radiobutton
        /// </summary>
        public IRadiobutton SicRadiobutton => new Radiobutton(Container, SicRadiobuttonLocator);

        /// <summary>
        /// Industry Search Box
        /// </summary>
        public ITextbox IndustrySearchBox => new Textbox(Container, IndustrySearchBoxLocator);

        /// <summary>
        /// Cancel Button
        /// </summary>
        public IButton SaveButton => new Button(Container, SaveButtonLocator);

        /// <summary>
        /// Cancel Button
        /// </summary>
        public IButton CancelButton => new Button(Container, CancelLocator);

        /// <summary>
        /// Industry Result Items
        /// </summary>
        public List<LitigationAnalyticsIndustryItem> IndustryResultItems => new ItemsCollection<LitigationAnalyticsIndustryItem>(Container, IndustryResultItemLocator).ToList();
    }
}