namespace Framework.Common.UI.Products.WestLawNextCanada.DropDowns
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// This is DetailDropdown in the toolbar
    /// </summary>
    public class CanadaDetailDropdown : DetailDropdown
    {
        private readonly By dropdownLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanadaDetailDropdown"/> class.
        /// </summary>
        /// <param name="container">Container</param>
        /// <param name="detailDropdownLocator">Detail dropdown element</param>
        public CanadaDetailDropdown(By container, By detailDropdownLocator) =>
            this.dropdownLocator = new ByChained(container, detailDropdownLocator);

        /// <summary>
        /// Verifies if Detail drop down is displayed link displayed
        /// </summary>
        /// <returns>The instance of the page</returns>
        /// <returns></returns>
        public override bool IsDisplayed() => DriverExtensions.GetElement(dropdownLocator).IsDisplayed();
    }
}