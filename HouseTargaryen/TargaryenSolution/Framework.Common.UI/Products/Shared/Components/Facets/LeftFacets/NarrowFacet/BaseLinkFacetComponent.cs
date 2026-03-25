namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// BaseLinkFacetComponent
    /// </summary>
    public abstract class BaseLinkFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask = ".//label[contains(text(),'{0}')]/preceding-sibling::input";

        private const string CheckBoxLabelLctMask = "//label[contains(text(),'{0}')]";

        private static readonly By LinkLocator = By.ClassName("co_facet_selectLink");

        /// <summary>
        /// Get results count 
        /// </summary>
        public int GetCheckBoxResultsCount(string checkBoxName) => this.GetCheckboxCount(string.Format(CheckBoxLabelLctMask, checkBoxName));

        /// <summary>
        /// IsSelectLinkDisplayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSelectLinkDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, LinkLocator);

        /// <summary>
        /// IsCheckboxSelected
        /// </summary>
        /// <param name="checkbox">The checkbox.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(string checkbox)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, checkbox))));

        /// <summary>
        /// Gets whether the given facet is checked under the Treatment Status section
        /// </summary>
        /// <param name="checkbox">The checkbox.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxDisplayed(string checkbox)
            => DriverExtensions.IsDisplayed(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, checkbox)));

        /// <summary>
        /// UnsetCheckbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="checkbox">The checkbox.</param>
        /// <returns>The new instance of T page.</returns>
        public T UnsetCheckbox<T>(string checkbox) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, checkbox))), false);

        /// <summary>
        /// Click on the Select link
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <returns> A new instance of the T page </returns>
        public T ClickSelectLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), LinkLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}