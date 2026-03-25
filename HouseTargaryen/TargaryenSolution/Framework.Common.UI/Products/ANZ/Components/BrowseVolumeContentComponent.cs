namespace Framework.Common.UI.Products.ANZ.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Browse Volume Content Component
    /// </summary>
    public class BrowseVolumeContentComponent : BaseModuleRegressionComponent
    {
        private const string VolumeRangeCheckboxLctMask = ".//a[text()='{0}']//preceding-sibling :: input";

        private static readonly By ContainerLocator = By.XPath("//*[@id='co_categoryPageCheckboxContainer']//div[contains(@class,'co_browseContent')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks Volume Range option checkbox
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the button</typeparam>
        /// <param name="volumeRangeOptionText">Volume range option text</param>
        /// <returns>New instance of T</returns>
        public T ClickVolumeRangeOption<T>(string volumeRangeOptionText) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(
                this.ComponentLocator,
                By.XPath(string.Format(VolumeRangeCheckboxLctMask, volumeRangeOptionText))).SetCheckbox(true);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
