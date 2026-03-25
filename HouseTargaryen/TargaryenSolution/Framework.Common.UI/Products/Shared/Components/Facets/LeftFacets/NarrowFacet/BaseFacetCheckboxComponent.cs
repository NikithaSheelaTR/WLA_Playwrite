namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// BaseFacetCheckboxComponent
    /// </summary>
    public abstract class BaseFacetCheckboxComponent : BaseFacetComponent
    {
        private const string CheckboxCountLcrMask = "({0})/../span[@class='co_facetCount']";

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="locator">The locator.</param>
        /// <returns>The <see cref="int"/>.</returns>
        protected int GetCheckboxCount(string locator)
            => DriverExtensions.GetText(By.XPath(string.Format(CheckboxCountLcrMask, locator))).ConvertCountToInt();

        /// <summary>
        /// Verify that the given facet is of the checkbox type
        /// The 'role' attribute for the RI pages
        /// The 'type' attribute for the other pages
        /// </summary>
        /// <param name="checkbox">The checkbox.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected bool IsCheckbox(IWebElement checkbox)
            => string.Equals(checkbox.GetAttribute("role"), "checkbox")
                || string.Equals(checkbox.GetAttribute("type"), "checkbox");

        /// <summary>
        /// Is Checkbox Selected
        /// The 'data-facetselected' attribute for the RI pages
        /// </summary>
        /// <param name="checkbox">The checkbox.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected bool IsCheckboxSelected(IWebElement checkbox)
            => string.Equals(checkbox.GetAttribute("data-facetselected"), "true")
                || DriverExtensions.IsCheckboxSelected(checkbox);

        /// <summary>
        /// Get the text of the checkbox
        /// </summary>
        /// <param name="checkbox">The checkbox.</param>
        /// <returns>The <see cref="string"/>.</returns>
        protected string GetCheckboxText(IWebElement checkbox) => checkbox.GetText();

        /// <summary>
        /// Is Checkbox Displayed
        /// </summary>
        /// <param name="checkbox">The label text of the checkbox</param>
        /// <returns>True if the facet is a checkbox</returns>
        protected bool IsCheckboxDisplayed(IWebElement checkbox) => checkbox.IsDisplayed();

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="checkbox">The checkbox.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        protected T SetCheckbox<T>(IWebElement checkbox, bool setTo) where T : ICreatablePageObject
        {
            if (this.IsCheckboxSelected(checkbox) != setTo)
            {
                checkbox.Click();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Expand facets until the specific one is not displayed
        /// </summary>
        /// <param name="checkboxLocator">The specific checkbox locator</param>
        /// <param name="expandButtonLocator">The expand Button Locator.</param>
        protected void ExpandParentFacet(By checkboxLocator, By expandButtonLocator)
        {
            while (DriverExtensions.SafeGetElement(DriverExtensions.GetElement(this.ComponentLocator), expandButtonLocator) != null
                   && !DriverExtensions.IsDisplayed(this.ComponentLocator, checkboxLocator))
            {
                DriverExtensions.GetElement(this.ComponentLocator, expandButtonLocator).CustomClick();
                DriverExtensions.WaitForJavaScript();
            }
        }
    }
}