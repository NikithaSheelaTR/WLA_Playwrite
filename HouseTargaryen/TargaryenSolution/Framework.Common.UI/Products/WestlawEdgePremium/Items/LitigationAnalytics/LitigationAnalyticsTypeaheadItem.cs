namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// LitigationAnalyticsTypeaheadItem
    /// </summary>
    public class LitigationAnalyticsTypeaheadItem : BaseItem
    {
        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="LitigationAnalyticsTypeaheadItem"/> class. 
        /// </summary>
        /// <param name="container"></param>
        public LitigationAnalyticsTypeaheadItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Nmae link
        /// </summary>
        public ILink NameLink => new Link(this.Container);

        /// <summary>
        /// is current item displayed
        /// </summary>
        /// <returns></returns>
        public bool IsCurrentItemDisplayed() =>
            DriverExtensions.IsDisplayed(this.Container);
    }
}