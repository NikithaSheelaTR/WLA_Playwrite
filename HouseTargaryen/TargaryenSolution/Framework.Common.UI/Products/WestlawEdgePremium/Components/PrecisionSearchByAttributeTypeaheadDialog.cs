namespace Framework.Common.UI.Products.WestlawEdgePremium.Components
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Precision Search By Attribute typehead dialog
    /// </summary>
    public class PrecisionSearchByAttributeTypeaheadDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='Typeahead-wrapper']");
        private static readonly By AdditionalFiltersSectionLocator = By.XPath("./div[not(contains(@class, 'PrecisionSearch-list'))]");
        private static readonly By FiltersSectionLocator = By.XPath("./*[@class='PrecisionSearch-list']");
        private static readonly By ZeroStateMessageLocator = By.XPath(".//*[contains(text(), 'No matches')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionSearchByAttributeTypeaheadDialog"/> class.
        /// </summary>
        public PrecisionSearchByAttributeTypeaheadDialog() => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Zero state message label
        /// </summary>
        public ILabel ZeroStateMessageLabel => new Label(ContainerLocator, ZeroStateMessageLocator);

        /// <summary>
        /// Matches
        /// </summary>
        public PrecisionMatchesComponent Matches => new PrecisionMatchesComponent(new ByChained(ContainerLocator, FiltersSectionLocator));

        /// <summary>
        /// Additional matches
        /// </summary>
        public PrecisionAdditionalMatchesComponent AdditionalMatches => new PrecisionAdditionalMatchesComponent(new ByChained(ContainerLocator, AdditionalFiltersSectionLocator));
    }
}
