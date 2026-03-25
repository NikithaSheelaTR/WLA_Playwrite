namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.ItDepends
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    using OpenQA.Selenium;

    /// <summary>
    /// It Depends typeAhead dialog
    /// </summary>
    public class ItDependsTypeAheadDialog : BaseModuleRegressionDialog
    {
        private static readonly By ItDependsLinkLocator = By.XPath("//a[contains(@class,'co-Typeahead-itDependsAnchorItem')]");
        private static readonly By Container = By.XPath("//div[contains(@class, 'co-TRDiscover-detail') or @id = 'contentTypeDetailsContainer']");

        /// <summary>
        /// Initializes a new instance of the <see cref="ItDependsTypeAheadDialog"/> class.
        /// </summary>
        public ItDependsTypeAheadDialog()
        {
            DriverExtensions.WaitForElementDisplayed(Container);
        }

        /// <summary>
        /// It depends link
        /// </summary>
        public ILink ItDependsLink => new Link(ItDependsLinkLocator);   
    }
}
