namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// MoreLinkFacetComponent
    /// facet has More link and can be expanded
    /// </summary>
    public abstract class MoreLinkFacetComponent : BaseFacetCheckboxComponent
    {
        // LctMask should have " because facet can have ' in its name
        private const string CheckboxLctMask = "//label[text()=\"{0}\"]/preceding-sibling::*[self::span or self::input]";

        private static readonly By ExpandButtonLocator = By.XPath(".//a[contains(@class,'co_facet_expand')]");

        private static readonly By MoreLinkLocator = By.XPath(".//li[contains(@id,'Facethierarcyclientanchor')]/a[contains(text(),'More')]");

        /// <summary>
        /// Verify if facet's checkbox is selected
        /// </summary>
        /// <param name="enumeration">The enumeration.</param>
        /// <typeparam name="TEnum">The enumeration type</typeparam>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected<TEnum>(TEnum enumeration) where TEnum : struct
            => base.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, EnumExtension.GetEnumTextValue(enumeration)))));

        /// <summary>
        /// Is Checkbox Displayed
        /// </summary>
        /// <param name="checkbox">The checkbox</param>
        /// <returns>True if the facet is a checkbox</returns>
        public bool IsCheckboxDisplayed(string checkbox)
            => this.IsCheckboxDisplayed(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, checkbox))));

        /// <summary>
        /// Click on the More link
        /// </summary>
        public void ClickMoreLink()
        {
            IWebElement link = DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), MoreLinkLocator);
            link.ScrollToElement();
            link.Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// IsMoreLinkDisplayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsMoreLinkDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, MoreLinkLocator);

        /// <summary>
        /// Apply the specified checkbox
        /// This method does not use BaseFacetCheckboxComponent.SetCheckbox because we get: 'Element is not clickable at point'
        /// while click on the Practice Series facet. But the Element exists and its locator is correct
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="checkbox">The checkbox.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(string checkbox, bool setTo) where T : ICreatablePageObject
        {
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, checkbox));

            if (this.IsMoreLinkDisplayed() && !this.IsCheckboxDisplayed(checkbox))
            {
                this.ClickMoreLink();
            }

            this.ExpandParentFacet(checkboxLocator, ExpandButtonLocator);
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), checkboxLocator).SetCheckbox(setTo);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}