namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Models.QuickCheck;

    using OpenQA.Selenium;

    /// <summary>
    /// AdditionalFeaturesDeliveryComponent
    /// </summary>
    public class AdditionalFeaturesDeliveryComponent : BaseModuleRegressionComponent
    {
        private static readonly By QuotationsLocator = By.Id("coid_layoutIncludeUnmatchedQuotations");
        private static readonly By CitedAuthorityOptionLocator = By.Id("coid_layoutIncludeCasesWithNoNegativeTreatment");
        private static readonly By AdditionalCasesOptionLocator = By.Id("coid_layoutIncludeAdditionalCases");
        private static readonly By ContainerLocator = By.Id("coid_includeTheseElementsSection");

        /// <summary>
        /// additional features locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Quotations option
        /// </summary>
        public AdditionalFeatureItem QuotationsOption => new AdditionalFeatureItem(QuotationsLocator);

        /// <summary>
        /// Cited Authority Option
        /// </summary>
        public AdditionalFeatureItem CitedAuthorityOption => new AdditionalFeatureItem(CitedAuthorityOptionLocator);

        /// <summary>
        /// Additional Cases Option
        /// </summary>
        public AdditionalFeatureItem AdditionalCasesOption => new AdditionalFeatureItem(AdditionalCasesOptionLocator);
    }
}
