namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Document information component in Recommendation tab
    /// </summary>
    public class RecommendationsDocumentInformationComponent: BaseModuleRegressionComponent
    {
        private static readonly By EditButtonLocator = By.XPath("//button[contains(@class, 'DA-EditDetailsButton')]");
        private static readonly By ContainerLocator = By.XPath("//div[@class='a11yExpandBox DA-AnalyzedDocument']");
        private static readonly By EditedLabelLocator = By.XPath(".//span[contains(text(), 'Edited')]");

        private EnumPropertyMapper<RecommendationDocumentDetail, WebElementInfo> RecommendationDocumentDetailMap =>
            EnumPropertyModelCache.GetMap<RecommendationDocumentDetail, WebElementInfo>(
                string.Empty,
                @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

        /// <summary>
        /// Edit button
        /// </summary>
        public IButton EditButton => new Button(this.ComponentLocator, EditButtonLocator);

        /// <summary>
        /// Edit button
        /// </summary>
        public ILabel EditedLabel => new Label(this.ComponentLocator, EditedLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get RecommendationDocumentInformation
        /// </summary>
        /// <param name="detail"> detail</param>
        /// <returns>Recommendation Document Details info</returns>
        public string GetDetail(RecommendationDocumentDetail detail) => DriverExtensions.GetElement(By.XPath(this.RecommendationDocumentDetailMap[detail].LocatorString)).Text
                .Replace(this.RecommendationDocumentDetailMap[detail].Text, string.Empty);
    }
}
