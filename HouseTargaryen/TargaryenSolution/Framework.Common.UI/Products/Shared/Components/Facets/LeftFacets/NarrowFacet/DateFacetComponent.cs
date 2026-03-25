namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Date Facet (Filter) on the Search Results Page
    /// </summary>
    public class DateFacetComponent : BaseFacetComponent
    {
        private const string SpecificDateContainerLctMask = "//div[contains(@id,'facet_div_') and @class ='co_divider' and .//span[text() = '{0}']]";

        private static readonly By DateFacetContainerLocator = By.CssSelector("#facet_div_Date, #facet_div_enhancedDate, #facet_div_date, #co_dateFacet, #co_researchDateFacet");

        private static readonly By DateDropdownContainerLocator = By.XPath(".//div[@id='co_dateWidget_RelatedInfo' or @id = 'coid_lightboxDateWidget']");

        private static readonly By DateFilterLabelLocator = By.XPath("//*[@id='facet_div_date']//span[@class='SearchFacet-buttonText']");

        private static readonly By ChangeDateButtonLocator = By.XPath("//button[@title='Date Change date']");

        private readonly By containerLocator;

        private IWebElement Container => DriverExtensions.GetElement(this.containerLocator);

        private IWebElement DropDownContainer => DriverExtensions.GetElement(this.Container, DateDropdownContainerLocator);

        /// <summary>
        ///  Date filter label on the filter panel on current history page
        /// </summary>
        public ILabel DateFilterLabel => new Label(DateFilterLabelLocator);

        /// <summary>
        /// Date dropdown
        /// </summary>
        protected override By ComponentLocator => this.containerLocator;

        /// <summary>
        /// Date Facet component
        /// </summary>
        public DateFacetComponent()
        {
            this.containerLocator = DateFacetContainerLocator;
        }

        /// <summary>
        /// Date Facet component
        /// </summary>
        public DateFacetComponent(string specificDate)
        {
            this.containerLocator = By.XPath(string.Format(SpecificDateContainerLctMask, specificDate));
        }

        /// <summary>
        /// Date dropdown
        /// </summary>
        public DateDropdown DateDropdown => new DateDropdown(this.DropDownContainer);

        /// <summary>
        /// scroll to date dropdown
        /// </summary>
        public void ScrollToDateDropdown()
        {
            if (!DropDownContainer.Displayed)
            {
                DriverExtensions.ScrollTo(DateDropdownContainerLocator);
            }
        }

        /// <summary>
        /// Clicks the "Change Date" button and returns the date dropdown.
        /// </summary>
        /// <returns>
        /// A <see cref="Dropdown"/> representing the date dropdown after the button is clicked.
        /// </returns>
        public DateDropdown ClickChangeDateButton()
        {
            DriverExtensions.Click(this.Container, ChangeDateButtonLocator);
            DriverExtensions.WaitForJavaScript();   
            return new DateDropdown(DateDropdownContainerLocator);
        }
    }
}