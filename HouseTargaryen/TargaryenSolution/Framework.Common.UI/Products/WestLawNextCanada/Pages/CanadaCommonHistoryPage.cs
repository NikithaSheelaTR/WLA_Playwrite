namespace Framework.Common.UI.Products.WestLawNextCanada.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.History;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Canada Common History Page
    /// </summary>
    public class CanadaCommonHistoryPage : EdgeCommonHistoryPage
    {
        private static readonly By TitleLabelLocator = By.CssSelector("h1.co_historyTitle");

        /// <summary>
        /// History title
        /// </summary>
        public ILabel TitleLabel => new Label(TitleLabelLocator);

        /// <summary>
        /// Gets the historyType enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<HistoryType, WebElementInfo> HistoryTypeMap
            => EnumPropertyModelCache.GetMap<HistoryType, WebElementInfo>();

        /// <summary>
        /// Choose History Type if not selected
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="historyType">
        /// History Type
        /// </param>
        /// <returns>
        /// new History Page 
        /// </returns>
        public T ChooseHistoryType<T>(HistoryType historyType) where T : ICommonHistoryPage
        {
            IWebElement historyTypeElement = DriverExtensions.WaitForElement(By.Id(this.HistoryTypeMap[historyType].Id));
            if (!DriverExtensions.GetAttribute("class", By.Id(this.HistoryTypeMap[historyType].Id)).Contains("co_hideState"))
            {
                historyTypeElement.Click();
                DriverExtensions.WaitForAnimation();
            }
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
