namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// PublicationFacet
    /// </summary>
    public class PublicationNameFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxLctMask = "//li[contains(@id,'co_facet') and ./*[contains(text(),'{0}')]]/*[local-name()='a' or local-name()='input']";

        private static readonly By ContainerLocator = By.Id("facet_div_publication");

        private static readonly By SelectPublicationNameLinkLocator = By.XPath("//*[@id='coid_publicationSelectTitle_publication'] | //*[@id='publication']/a");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Is Checkbox Selected
        /// </summary>
        /// <param name="checkbox">The checkbox.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(string checkbox)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, checkbox))));

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="checkbox">The checkbox.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public new int GetCheckboxCount(string checkbox)
            => base.GetCheckboxCount(string.Format(CheckboxLctMask, checkbox));

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="checkbox">The checkbox.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(string checkbox, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, checkbox))), setTo);

        /// <summary>
        /// Is Checkbox Displayed
        /// </summary>
        /// <param name="checkbox">The label text of the checkbox</param>
        /// <returns>True if the facet is a checkbox</returns>
        public bool IsCheckboxDisplayed(string checkbox)
            => this.IsCheckboxDisplayed(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxLctMask, checkbox))));

        /// <summary>
        /// Click on the 'Select Publication Name' link
        /// </summary>
        /// <returns> The <see cref="PublicationNameDialog"/>. </returns>
        public PublicationNameDialog ClickSelectPublicationNameLink()
        {
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), SelectPublicationNameLinkLocator).Click();
            return new PublicationNameDialog();
        }
    }
}