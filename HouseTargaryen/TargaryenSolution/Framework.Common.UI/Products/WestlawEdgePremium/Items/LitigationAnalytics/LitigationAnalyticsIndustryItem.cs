using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Items;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdgePremium.Items.LitigationAnalytics
{
    /// <summary>
    /// Litigation Analytics Industry Item
    /// </summary>
    public class LitigationAnalyticsIndustryItem : BaseItem
    {
        private static readonly By IndustryNameLocator = By.XPath(".//span");

        /// <summary>
        ///  Litigation Analytics Industry Item
        /// </summary>
        /// <param name="container"></param>
        public LitigationAnalyticsIndustryItem(IWebElement container) : base(container)
        {
        }
        /// <summary>
        /// Get Industry Name
        /// </summary>
        public string GetIndustryName => DriverExtensions.GetElement(this.Container, IndustryNameLocator).Text;

        /// <summary>
        /// Industry Name
        /// </summary>
        public IButton IndustryName => new Button(this.Container);
    }
}