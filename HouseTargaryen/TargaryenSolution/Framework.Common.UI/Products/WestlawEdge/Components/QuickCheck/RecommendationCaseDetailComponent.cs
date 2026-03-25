namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// RecommendationCaseDetailComponent for Opponent view
    /// </summary>
    public class RecommendationCaseDetailComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//div[@class='co_searchContent DA-SearchContent']//div[@class='DA-CaseDetails']");

        private IWebElement ContainerElement;
        
        private EnumPropertyMapper<RecommendationCaseDetail, WebElementInfo> RecommendationCaseDetailMap =>
            EnumPropertyModelCache.GetMap<RecommendationCaseDetail, WebElementInfo>(
                string.Empty,
                @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

        /// <summary>
        /// RecommendationCaseDetailComponent
        /// </summary>
        public RecommendationCaseDetailComponent(IWebElement containerElement)
        {
            this.ContainerElement = containerElement;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get RecommendationCaseDetail
        /// </summary>
        /// <param name="detail"> detail</param>
        /// <returns>Recommendation Case Detail info</returns>
        public string GetDetail(RecommendationCaseDetail detail) => DriverExtensions.GetElement(this.ContainerElement, this.ComponentLocator, By.XPath(this.RecommendationCaseDetailMap[detail].LocatorString)).Text
                .Replace(this.RecommendationCaseDetailMap[detail].Text, string.Empty);
    }
}
