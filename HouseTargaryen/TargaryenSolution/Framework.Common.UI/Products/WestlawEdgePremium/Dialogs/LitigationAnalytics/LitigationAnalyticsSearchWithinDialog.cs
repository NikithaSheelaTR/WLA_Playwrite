namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using OpenQA.Selenium;

    /// <summary>
    /// Litigation Analytics Search within dialog.
    /// </summary>
    public class LitigationAnalyticsSearchWithinDialog : EdgeSearchWithinDialog
    {
        private static readonly By SearchInputLocator = By.XPath("//input[@name = 'SearchFacetSearchWithinOverlay-inputKeyword']");
        private static readonly By SearchButtonLocator = By.XPath("//*[contains(@class,'SearchFacet-button SearchFacet-buttonSubmit SearchFacet-buttonSearch')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="LitigationAnalyticsSearchWithinDialog"/> class. 
        ///  </summary>
        /// <param name="additionalInfo"> additional Info </param>
        public LitigationAnalyticsSearchWithinDialog(string additionalInfo) : base(additionalInfo)
        {
        }

        /// <summary>
        /// SearchInput textbox
        /// </summary>
        public ITextbox SearchTextbox => new SearchWithinTextbox(SearchInputLocator);

        /// <summary>
        /// Search Button 
        /// </summary>
        public IButton SearchButton => new Button(SearchButtonLocator);
    }
}