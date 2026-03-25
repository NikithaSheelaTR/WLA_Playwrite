namespace Framework.Common.UI.Products.Shared.Pages
{
    using Framework.Common.UI.Products.Shared.Components.HomePage;
    using Framework.Common.UI.Products.Shared.Components.RightPaneComponents;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Common Page for Migrated Tax Pages
    /// </summary>
    public class CommonMigratedTaxPage : TableOfContentsBrowsePage
    {
        private static readonly By PwcTaxLogoLocator = By.XPath("//img[@class='co_floatRight']");

        /// <summary>
        /// Custom Pages Widget
        /// </summary>
        public CustomPagesComponent CustomPagesWidget { get; } = new CustomPagesComponent();

        /// <summary>
        /// Find And Key Cite Widget
        /// </summary>
        public FindAndKeyCiteATaxDocumentComponent FindAndKeyCiteATaxDocument { get; } = new FindAndKeyCiteATaxDocumentComponent();

        /// <summary>
        /// Reference Attorney Hotline Widget
        /// </summary>
        public ReferenceAttorneyHotlineComponent ReferenceAttorneyHotline { get; } = new ReferenceAttorneyHotlineComponent();

        /// <summary>
        /// Resources Widget
        /// </summary>
        public ResourcesComponent Resources { get; } = new ResourcesComponent();

        /// <summary>
        /// Get Logo Alt Text (for PWC and EY pages)
        /// </summary>
        /// <returns>logo alt text</returns>
        public string GetPageLogoAltText() => DriverExtensions.GetElement(PwcTaxLogoLocator).GetAttribute("alt");

        /// <summary>
        /// Is Logo Displayed (for PWC and EY pages)
        /// </summary>
        /// <returns>true if logo is displayed</returns>
        public bool IsLogoDisplayed() => DriverExtensions.IsDisplayed(PwcTaxLogoLocator, 5);
    }
}
