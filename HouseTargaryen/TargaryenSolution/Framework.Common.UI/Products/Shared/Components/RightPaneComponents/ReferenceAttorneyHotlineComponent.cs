namespace Framework.Common.UI.Products.Shared.Components.RightPaneComponents
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// PWC Reference Attorney Hotline for PWC/KPMG Tax pages
    /// </summary>
    public class ReferenceAttorneyHotlineComponent : BaseModuleRegressionComponent
    {
        private static readonly By ReferenceAttorneyHotlineContentLocator = By.XPath("//div[@class='co_genericBoxContent']//h2 | //div[@class='co_genericBoxContent']//h3");

        private static readonly By ContainerLocator = By.Id("coid_website_browseRightColumn_widget_0");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Number Label Text
        /// </summary>
        /// <returns> Number from Reference Attorney Hotline Component</returns>
        public string GetNumberLabelText() => DriverExtensions.GetText(ReferenceAttorneyHotlineContentLocator);
    }
}