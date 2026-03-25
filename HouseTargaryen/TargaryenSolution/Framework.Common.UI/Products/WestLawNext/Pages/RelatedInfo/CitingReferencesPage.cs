namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Document.RI;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The citing references page 
    /// </summary>
    public class CitingReferencesPage : TabPage
    {
        private const string CitingReferenceGridLctMask = "//table[@id='co_relatedInfo_table_citingRefs']";

        private static readonly By ResultContainerLocator = By.Id("co_contentColumn");
        private static readonly By LoadingSpinnerLabelLocator = By.XPath("//div[contains(@class,'coid_relatedInfo_loading_spinner_div') and contains(@style,'display: none;')]");

        /// <summary>
        /// Loading spinner label
        /// </summary>
        public ILabel LoadingSpinnerLabel => new Label(LoadingSpinnerLabelLocator);

        /// <summary>
        /// Gets or sets grid for citing references
        /// </summary>
        public ReferenceGridComponent CitingReferenceGrid { get; set; }
            = new ReferenceGridComponent(CitingReferenceGridLctMask);

        /// <summary>
        /// Get Citing References Grid Element Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCitingReferencesGridElementText()
            => DriverExtensions.WaitForElement(ResultContainerLocator).Text;

        /// <summary>
        /// Is Citing References Grid Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCitingReferencesGridElementDisplayed() => DriverExtensions.IsDisplayed(ResultContainerLocator);
    }
}